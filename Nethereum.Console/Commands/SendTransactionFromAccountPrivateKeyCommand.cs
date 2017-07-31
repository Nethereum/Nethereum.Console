using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.CommandLineUtils;
using Nethereum.Web3.Accounts;

namespace Nethereum.Console
{
    public class SendTransactionFromAccountPrivateKeyCommand : SendTransactionBaseCommand
    {
        protected PrivateKeyCommandOption _privateKeyCommandOption;

        public SendTransactionFromAccountPrivateKeyCommand(IAccountService accountService) : base(accountService)
        {
            Name = "send-transaction-from-account-private-key";
            Description = "Sends a transaction using the account's private key";

            HelpOption("-? | -h | --help");
            OnExecute((Func<int>)RunCommand);
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
                _transactionCommandOptions.ParseAndValidateInput();
                _privateKeyCommandOption.ParseAndValidateInput();

                if (_transactionCommandOptions.HasInputErrors) return 1;
                if (_privateKeyCommandOption.HasInputErrors) return 1;


                var txn = accountService.SendTransactionAsync(new Account(_privateKeyCommandOption.PrivateKey),
                                                        _transactionCommandOptions.ToAddress,
                                                        _transactionCommandOptions.Amount ?? _transactionCommandOptions.Amount.Value,
                                                        _transactionCommandOptions.RpcAddress,
                                                        _transactionCommandOptions.Gas,
                                                        _transactionCommandOptions.GasPrice,
                                                        _transactionCommandOptions.Data
                                                        ).Result;

                System.Console.WriteLine(txn + " transaction submitted");
                return 0;

            }
            catch (Exception ex)
            {
                System.Console.WriteLine("Error: " + ex.Message);
                System.Console.WriteLine("Stack trace: " + ex.StackTrace);
                return 1;
            }

            return 0;
        }
    }
}
