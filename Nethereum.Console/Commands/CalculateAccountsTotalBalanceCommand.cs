﻿using System;
using Microsoft.Extensions.CommandLineUtils;

namespace Nethereum.Console
{
    public class CalculateAccountsTotalBalanceCommand : CommandLineApplication
    {
        private readonly CommandOption _addresses;
        private readonly CommandOption _rpcAddress;

        public CalculateAccountsTotalBalanceCommand()
        {
            Name = "accounts-total-balance";
            Description = "Calculates the total Ether balance of an account or accounts using the addresses provided";
            _addresses = Option("-a | --addr", "The address or addresses to calculate the total balance", CommandOptionType.MultipleValue);
            _rpcAddress = this.AddOptionRpcAddress();
           
            HelpOption("-? | -h | --help");
            OnExecute((Func<int>)RunCommand);
        }

        private int RunCommand()
        {
            var addresses = _addresses.Values;
            if (addresses.Count == 0)
            {
                System.Console.WriteLine("No addressed provided");
                return 1;
            }

            var rpcAddress = _rpcAddress.Value();
            if (string.IsNullOrWhiteSpace(rpcAddress))
            {
                System.Console.WriteLine("The rpcAddress was not specified");
                return 1;
            }

            IAccountService accountService = new AccountService();
            var balance = accountService.CalculateTotalBalanceAccountsAsync(rpcAddress, addresses).Result;
            System.Console.WriteLine("Total Balance: " + balance);

            return 0;
        }
    }
}