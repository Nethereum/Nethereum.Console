using Microsoft.Extensions.CommandLineUtils;

namespace Nethereum.Console
{
    public class App : CommandLineApplication
    {
        public App()
        {
            var accountService = new AccountService();

            Commands.Add(new CreateAccountCommand());
            Commands.Add(new SendTransactionFromAccountFileCommand(accountService));
            Commands.Add(new SendTransactionFromAccountPrivateKeyCommand(accountService));
            Commands.Add(new TransferEtherFromAccountFileCommand(accountService));
            Commands.Add(new TransferEtherFromAccountPrivateKeyCommand(accountService));
            Commands.Add(new CreateAccountsAndMixBalancesCommand());
            Commands.Add(new CalculateAccountsFolderTotalBalanceCommand());
            Commands.Add(new CalculateAccountsTotalBalanceCommand());
            Commands.Add(new AccountTokenBalanceCommand(accountService));
            Commands.Add(new CreateAccountKeyPairCommand(accountService));
            
            HelpOption("-h | -? | --help");
        }
    }
}