using Microsoft.Extensions.CommandLineUtils;

namespace Nethereum.Console
{
    public class PrivateKeyCommandOption: ICommandOption
    {
        public CommandOption PrivateKeyOption { get; set; }
        public string PrivateKey { get; set; }

        public bool HasInputErrors { get; protected set; }

        public void AddOptionToCommandLineApplication(CommandLineApplication commandLineApplication)
        {
            PrivateKeyOption = commandLineApplication.AddOptionPrivateKey();
        }

        public void ParseAndValidateInput()
        {
            PrivateKey = PrivateKeyOption.TryParseRequiredString(HasInputErrors);
        }
    }
}