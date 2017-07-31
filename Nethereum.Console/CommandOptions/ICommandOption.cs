using Microsoft.Extensions.CommandLineUtils;

namespace Nethereum.Console
{
    public interface ICommandOption
    {
        bool HasInputErrors { get; }

        void AddOptionToCommandLineApplication(CommandLineApplication commandLineApplication);
        void ParseAndValidateInput();
    }
}