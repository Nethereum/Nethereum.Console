using Microsoft.Extensions.CommandLineUtils;

namespace Nethereum.Console
{
    public class App : CommandLineApplication
    {
        public App()
        {
            Commands.Add(new CreateAccountCommand());
            Commands.Add(new CreateAccountsAndMixBalancesCommand());
            Commands.Add(new CalculateAccountsFolderTotalBalance());
            Commands.Add(new CalculateAccountsTotalBalance());
            

            HelpOption("-h | -? | --help");
        }
    }
}