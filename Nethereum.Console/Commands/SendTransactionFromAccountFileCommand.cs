using System;
using Microsoft.Extensions.CommandLineUtils;
using Nethereum.Web3.Accounts;

namespace Nethereum.Console
{
        public class SendTransactionFromAccountFileCommand : SendTransactionBaseCommand
        {
            protected AccountKeyStoreCommandOption _accountKeyStoreCommandOption;

            public SendTransactionFromAccountFileCommand(IAccountService accountService) : base(accountService)
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


                    var txn = accountService.SendTransactionAsync(_accountKeyStoreCommandOption.AccountFile,
                                                            _accountKeyStoreCommandOption.Password,
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
