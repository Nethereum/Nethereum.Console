using Microsoft.Extensions.CommandLineUtils;

namespace Nethereum.Console
{
    public class SimpleTransactionCommandOptions : RpcAddressCommandOption
    {
        public CommandOption ToAddressOption { get; set; }
        public CommandOption AmountOption { get; set; }

        public string ToAddress { get; set; }
        public decimal? Amount { get; set; }

        public bool IsToAddressRequired { get; set; }
        public bool IsAmountRequired { get; set; }

        protected IAccountService accountService;

        public SimpleTransactionCommandOptions(IAccountService accountService)
        {
            IsToAddressRequired = true;
            IsAmountRequired = true;

            this.accountService = accountService;
        }

        public override void AddOptionToCommandLineApplication(CommandLineApplication commandLineApplication)
        {
            base.AddOptionToCommandLineApplication(commandLineApplication);
            ToAddressOption = commandLineApplication.AddOptionDestinationAddress(IsToAddressRequired);
            AmountOption = commandLineApplication.AddOptionAmountInEther(IsAmountRequired);
        }

        public override void ParseAndValidateInput()
        {
            base.ParseAndValidateInput();
            Amount = AmountOption.TryParseAndValidateDecimal(HasInputErrors, IsAmountRequired);
            ToAddress = ToAddressOption.TryParseAndValidateAddress(accountService, HasInputErrors, IsToAddressRequired);
        }
    }
}