using System;
using Microsoft.Extensions.CommandLineUtils;

namespace Nethereum.Console
{
    public abstract class TransferEtherBaseCommand : CommandLineApplication
    {
        protected SimpleTransactionCommandOptions _simpleTransactionCommandOption;
        protected IAccountService accountService;

        public TransferEtherBaseCommand(IAccountService accountService)
        {
            this.accountService = accountService;
            InitOptions();
            HelpOption("-? | -h | --help");
            OnExecute((Func<int>)RunCommand);
        }

        protected virtual void InitOptions()
        {
            _simpleTransactionCommandOption = new SimpleTransactionCommandOptions(accountService);
            _simpleTransactionCommandOption.AddOptionToCommandLineApplication(this);
        }

        protected abstract int RunCommand();  
    }
}