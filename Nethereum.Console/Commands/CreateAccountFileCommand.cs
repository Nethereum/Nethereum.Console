using System;
using Microsoft.Extensions.CommandLineUtils;
using System.IO;

namespace Nethereum.Console
{
    public class CreateAccountFileCommand : CommandLineApplication
    {
        private readonly CommandOption _destinationDirectory;
        private readonly CommandOption _password;
        private readonly CommandOption _privateKey;
        private IAccountService accountService;

        public CreateAccountFileCommand(IAccountService accountService)
        {
            this.accountService = accountService;
            Name = "create-account-file";
            Description = "Creates an account key store file using the provided private key and encryption password";
            _password = this.AddOptionAccoutFilePassword();
            _privateKey = this.AddOptionPrivateKey();
            _destinationDirectory = Option("-dd | --destinationDirectory", "Optional: The folder to create the account file", CommandOptionType.SingleValue);

            HelpOption("-? | -h | --help");
            OnExecute((Func<int>)RunCommand);
        }

        private int RunCommand()
        {
            var hasErrors = false;

            var destinationFolder = _destinationDirectory.Value();
            if (string.IsNullOrWhiteSpace(destinationFolder))
            {
                destinationFolder = Directory.GetCurrentDirectory();
            }

            var password = _password.TryParseRequiredString(hasErrors);
            var privateKey = _privateKey.TryParseRequiredString(hasErrors);

            var account = accountService.CreateAccount(password, privateKey, destinationFolder);
            System.Console.WriteLine("Account file for: " + account.Address + " created at: " + destinationFolder);
            return 0;
        }
    }
}