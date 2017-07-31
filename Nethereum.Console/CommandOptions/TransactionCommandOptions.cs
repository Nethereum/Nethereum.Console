using Microsoft.Extensions.CommandLineUtils;
using Nethereum.Hex.HexTypes;

namespace Nethereum.Console
{
    public class TransactionCommandOptions: SimpleTransactionCommandOptions
    {
        public CommandOption GasOption { get; set; }
        public CommandOption GasPriceOption { get; set; }
        public CommandOption DataOption { get; set; }

        public HexBigInteger Gas { get; set; }
        public HexBigInteger GasPrice { get; set; }
        public string Data { get; set; }

        public TransactionCommandOptions(IAccountService accountService):base(accountService)
        {
            IsToAddressRequired = false;
            IsAmountRequired = false;
        }

        public override void AddOptionToCommandLineApplication(CommandLineApplication commandLineApplication)
        {
            base.AddOptionToCommandLineApplication(commandLineApplication);
            GasOption = commandLineApplication.AddOptionGas(false);
            GasPriceOption = commandLineApplication.AddOptionGasPrice(false);
            DataOption = commandLineApplication.AddOptionData(false);
        }

        public override void ParseAndValidateInput()
        {
            base.ParseAndValidateInput();
            Data = DataOption.Value();
            Gas = GasOption.TryParseHexBigIntegerValue(HasInputErrors, false);
            if (Gas != null && Gas.Value == 0) Gas = null;
            GasPrice = GasPriceOption.TryParseHexBigIntegerValue(HasInputErrors, false);
            if (GasPrice != null && GasPrice.Value == 0) GasPrice = null;
        }
    
    }
}