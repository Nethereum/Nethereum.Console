using System;
using Microsoft.Extensions.CommandLineUtils;
using Nethereum.RPC.Eth.DTOs;

namespace Nethereum.Console
{
    public class TransferEtherFromAccountFileCommand : TransferEtherBaseCommand
    {
        protected AccountKeyStoreCommandOption _accountKeyStoreCommandOption;

        public TransferEtherFromAccountFileCommand(IAccountService accountService) : base(accountService)
        {
            Name = "account-transfer-from-account-file";
            Description = "Transfers ether from a given acccount file (key store file) to another account";
        }

        protected override void InitOptions()
        {
            base.InitOptions();
            _accountKeyStoreCommandOption = new AccountKeyStoreCommandOption();
            _accountKeyStoreCommandOption.AddOptionToCommandLineApplication(this);
        }

        protected override int RunCommand()
        {
            try
            {
                _simpleTransactionCommandOption.ParseAndValidateInput();
                _accountKeyStoreCommandOption.ParseAndValidateInput();

                if (_simpleTransactionCommandOption.HasInputErrors) return 1;
                if (_accountKeyStoreCommandOption.HasInputErrors) return 1;


                var txn = accountService.TransferEtherAsync(_accountKeyStoreCommandOption.AccountFile,
                                                        _accountKeyStoreCommandOption.Password,
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