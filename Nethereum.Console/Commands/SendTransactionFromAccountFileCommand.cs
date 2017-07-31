using System;
using Microsoft.Extensions.CommandLineUtils;

namespace Nethereum.Console
{
    public abstract class SendTransactionBaseCommand : CommandLineApplication
    {
        protected TransactionCommandOptions _transactionCommandOptions;
        protected IAccountService accountService;

        public SendTransactionBaseCommand(IAccountService accountService)
        {
            this.accountService = accountService;   
            InitOptions();
            HelpOption("-? | -h | --help");
            OnExecute((Func<int>)RunCommand);
        }

        protected virtual void InitOptions()
        {
            _transactionCommandOptions = new TransactionCommandOptions(accountService);
            _transactionCommandOptions.AddOptionToCommandLineApplication(this);
        }

        protected abstract int RunCommand();

    }

    public class SendTransactionFromAccountFileCommand : SendTransactionBaseCommand
    {
        protected AccountKeyStoreCommandOption _accountKeyStoreCommandOption;

        public SendTransactionFromAccountFileCommand(IAccountService accountService):base(accountService)
        {
            Name = "send-transaction-from-account-file";
            Description = "Sends a transaction using the account's key store file";
            
            HelpOption("-? | -h | --help");
            OnExecute((Func<int>)RunCommand);
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
                _transactionCommandOptions.ParseAndValidateInput();
                _accountKeyStoreCommandOption.ParseAndValidateInput();

                if (_transactionCommandOptions.HasInputErrors) return 1;
                if (_accountKeyStoreCommandOption.HasInputErrors) return 1;


                var txn = accountService.SendTransaction(_accountKeyStoreCommandOption.AccountFile,
                                                        _accountKeyStoreCommandOption.Password,
                                                        _transactionCommandOptions.ToAddress,
                                                        _transactionCommandOptions.Amount ?? _transactionCommandOptions.Amount.Value,
                                                        _transactionCommandOptions.RpcAddress,
                                                        _transactionCommandOptions.Gas,
                                                        _transactionCommandOptions.GasPrice,
                                                        _transactionCommandOptions.Data
                                                        ).Result;

                System.Console.WriteLine(txn + " sent");
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