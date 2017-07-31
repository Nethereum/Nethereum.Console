using System;
using Microsoft.Extensions.CommandLineUtils;
using Nethereum.Hex.HexConvertors.Extensions;

namespace Nethereum.Console
{

    public class CreateAccountsAndMixBalancesCommand : CommandLineApplication
    {
        private readonly CommandOption _sourceDirectory;
        private readonly CommandOption _destinationDirectory;
        private readonly CommandOption _password;
        private readonly CommandOption _rpcAddress;
        private readonly CommandOption _numberOfAccounts;


        public CreateAccountsAndMixBalancesCommand()
        {
            Name = "create-acccounts-mix-balances";
            Description = "Generates new accounts in a given directory, and mixes and transfer balances from the accounts in the source directory";
            _sourceDirectory = Option("-sd | --sourceDirectory", "The directory containing the source accounts", CommandOptionType.SingleValue);
            _destinationDirectory = Option("-dd | --destinationDirectory", "The directory to create new accounts", CommandOptionType.SingleValue);
            _password = Option("-p | --password", "The generic password used for all the account files", CommandOptionType.SingleValue);
            _rpcAddress = Option("--url", "The rpc address to connect", CommandOptionType.SingleValue);
            _numberOfAccounts = Option("-na", "Optional: The number of accounts to create, defaults to 4", CommandOptionType.SingleValue);
            HelpOption("-? | -h | --help");
            OnExecute((Func<int>)RunCommand);
        }

        private int RunCommand()
        {
            var sourceFolder = _sourceDirectory.Value();
            if (string.IsNullOrWhiteSpace(sourceFolder))
            {
                System.Console.WriteLine("The source directory was not specified");
                return 1;
            }

            var destinationFolder = _destinationDirectory.Value();
            if (string.IsNullOrWhiteSpace(destinationFolder))
            {
                System.Console.WriteLine("The destination directory was not specified");
                return 1;
            }

            var password = _password.Value();
            if (string.IsNullOrWhiteSpace(password))
            {
                System.Console.WriteLine("The password was not specified");
                return 1;
            }

            var rpcAddress = _rpcAddress.Value();
            if (string.IsNullOrWhiteSpace(rpcAddress))
            {
                System.Console.WriteLine("The rpcAddress was not specified");
                return 1;
            }

            var numberOfAcccounts = 4;
            if (!string.IsNullOrWhiteSpace(_numberOfAccounts.Value()))
            {
                
                var passed =  int.TryParse(_numberOfAccounts.Value(), out numberOfAcccounts);
                if (!passed)
                {
                    System.Console.WriteLine("Number of accounts is not a valid number");
                    return 1;
                }
            }

            IAccountService accountService = new AccountService();
            new SimpleMixerService(accountService).CreateAccountsAndMixBalances(sourceFolder, password, destinationFolder, numberOfAcccounts, rpcAddress).Wait();
            return 0;
        }
    }
}