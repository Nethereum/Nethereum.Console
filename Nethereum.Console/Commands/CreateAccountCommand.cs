using System;
using System.IO;
using Microsoft.Extensions.CommandLineUtils;

namespace Nethereum.Console
{
    public class CreateAccountCommand : CommandLineApplication
    {
        private readonly CommandOption _destinationDirectory;
        private readonly CommandOption _password;
       
        public CreateAccountCommand()
        {
            Name = "create-account";
            Description = "Creates an account and stores it in a given directory";
            _password = Option("-p | --password", "The password used for the account files", CommandOptionType.SingleValue);
            _destinationDirectory = Option("-dd | --destinationDirectory", "Optional: The folder to create the account file", CommandOptionType.SingleValue);

            HelpOption("-? | -h | --help");
            OnExecute((Func<int>)RunCommand);
        }

        private int RunCommand()
        {
           
            var destinationFolder = _destinationDirectory.Value();
            if (string.IsNullOrWhiteSpace(destinationFolder))
            {
                destinationFolder = Directory.GetCurrentDirectory();
            }

            var password = _password.Value();
            if (string.IsNullOrWhiteSpace(password))
            {
                System.Console.WriteLine("The password was not specified");
                return 1;
            }

            IAccountService accountService = new AccountService();
            var account = accountService.CreateAccount(password, destinationFolder);
            System.Console.WriteLine("Account: " + account.Address + " created at: " + destinationFolder);
            return 0;
        }
    }
}