using Microsoft.Extensions.CommandLineUtils;

namespace Nethereum.Console
{
    public class AccountKeyStoreCommandOption: ICommandOption
    {
        public CommandOption  AccountFileOption { get; set; }
        public CommandOption  PasswordOption { get; set; }

        public string Password { get; set; }
        public string AccountFile { get; set; }

        public bool HasInputErrors { get; protected set; }

        public void AddOptionToCommandLineApplication(CommandLineApplication commandLineApplication)
        {
            AccountFileOption = commandLineApplication.AddOptionAccoutKeyStoreFile();
            PasswordOption = commandLineApplication.AddOptionAccoutFilePassword();
        }

        public void ParseAndValidateInput()
        {
            AccountFile = AccountFileOption.TryParseRequiredString(HasInputErrors);
            Password = PasswordOption.TryParseRequiredString(HasInputErrors);
        }
    }
}