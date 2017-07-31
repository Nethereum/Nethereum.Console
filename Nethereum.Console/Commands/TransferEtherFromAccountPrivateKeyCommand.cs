using System;
using Nethereum.Web3.Accounts;

namespace Nethereum.Console
{
    public class TransferEtherFromAccountPrivateKeyCommand : TransferEtherBaseCommand
    {
        protected PrivateKeyCommandOption _privateKeyCommandOption;

        public TransferEtherFromAccountPrivateKeyCommand(IAccountService accountService) : base(accountService)
        {
            Name = "account-transfer-from-account-private-key";
            Description = "Transfers ether using the account's private key";
        }

        protected override void InitOptions()
        {
            base.InitOptions();
            _privateKeyCommandOption = new PrivateKeyCommandOption();
            _privateKeyCommandOption.AddOptionToCommandLineApplication(this);
        }

        protected override int RunCommand()
        {
            try
            {
                _simpleTransactionCommandOption.ParseAndValidateInput();
                _privateKeyCommandOption.ParseAndValidateInput();

                if (_simpleTransactionCommandOption.HasInputErrors) return 1;
                if (_privateKeyCommandOption.HasInputErrors) return 1;


                var txn = accountService.TransferEtherAsync(new Account(_privateKeyCommandOption.PrivateKey),
                                                        _simpleTransactionCommandOption.ToAddress,
                                                        _simpleTransactionCommandOption.Amount.Value,
                                                        _simpleTransactionCommandOption.RpcAddress).Result;

                System.Console.WriteLine(txn + " submitted, to: " + _simpleTransactionCommandOption.ToAddress + ", sent amount: " + _simpleTransactionCommandOption.Amount.Value);
                return 0;

            }
            catch (Exception ex)
            {
                System.Console.WriteLine("Error: " + ex.Message);
                System.Console.WriteLine("Stack: " + ex.StackTrace.ToString());

                return 1;
            }
        }
    }
}