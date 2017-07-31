using System;
using System.IO;
using Microsoft.Extensions.CommandLineUtils;
using System.Numerics;

namespace Nethereum.Console
{
    public class CalculateAccountsFolderTotalBalanceCommand : CommandLineApplication
    {
        private readonly CommandOption _sourceDirectory;
        private readonly CommandOption _rpcAddress;

        public CalculateAccountsFolderTotalBalanceCommand()
        {
            Name = "accounts-dir-total-balance";
            Description = "Calculates the total Ether balance of all the accounts in a given directory";
            _sourceDirectory = Option("-sd | --sourceDirectory", "The directory containing the source accounts", CommandOptionType.SingleValue);
            _rpcAddress = this.AddOptionRpcAddress();
            
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

            var rpcAddress = _rpcAddress.Value();
            if (string.IsNullOrWhiteSpace(rpcAddress))
            {
                System.Console.WriteLine("The rpcAddress was not specified");
                return 1;
            }

            IAccountService accountService = new AccountService();
            var balance = accountService.CalculateTotalBalanceAccountsInFolder(rpcAddress, sourceFolder).Result;
            System.Console.WriteLine("Total Balance: " +  balance);

            return 0;
        }
    }
}