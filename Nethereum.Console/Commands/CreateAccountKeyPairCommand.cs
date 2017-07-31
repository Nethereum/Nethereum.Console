using System;
using Microsoft.Extensions.CommandLineUtils;

namespace Nethereum.Console
{
    public class CreateAccountKeyPairCommand: CommandLineApplication
    {
        private IAccountService accountService;
        public CreateAccountKeyPairCommand(IAccountService accountService)
        {
            this.accountService = accountService;
            Name = "create-account-key-pair";
            Description = "Generates a new private key and ethereum address";

            HelpOption("-? | -h | --help");
            OnExecute((Func<int>)RunCommand);
        }

        private int RunCommand()
        {
            var key = accountService.GenerateNewAccount();
            System.Console.WriteLine("Account Address: " + key.GetPublicAddress());
            System.Console.WriteLine("Private Key: " + key.GetPrivateKey());
            return 0;
        }

    }
}