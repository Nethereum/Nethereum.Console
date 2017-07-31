using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.CommandLineUtils;
using Nethereum.Web3.Accounts;

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
}
