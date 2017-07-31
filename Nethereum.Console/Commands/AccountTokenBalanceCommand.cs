using System;
using Microsoft.Extensions.CommandLineUtils;

namespace Nethereum.Console
{

    /// <example>
    ///  dotnet run account-total-token-balance --url "https://mainnet.infura.io:8545"  -a 0xd0a6e6c54dbc68db5db3a091b171a77407ff7ccf -ca 0x86fa049857e0209aa7d9e616f7eb3b3b78ecfdb0
    /// </example>
    public class AccountTokenBalanceCommand : CommandLineApplication
    {
        private readonly CommandOption _address;
        private readonly CommandOption _contractAddress;
        private readonly CommandOption _numberOfDecimals;
        private readonly CommandOption _rpcAddress;
        private IAccountService accountService;

        public AccountTokenBalanceCommand(IAccountService accountService)
        {
            this.accountService = accountService;
            Name = "account-total-token-balance";
            Description = "Returns the token balance of an account from a compliant ERC20 smart contract";
            _address = Option("-a | --addr", "The address to get the total balance", CommandOptionType.SingleValue);
            _contractAddress = Option("-ca | --contractAddr", "The contract address", CommandOptionType.SingleValue);
            _numberOfDecimals = Option("-dec | --decimalPlaces", "Optional: The number of decimal places to the conversion, defaults to 18", CommandOptionType.SingleValue);

            _rpcAddress = this.AddOptionRpcAddress();

            HelpOption("-? | -h | --help");
            OnExecute((Func<int>)RunCommand);
        }

        private int RunCommand()
        {
            var hasErrorInput = false;
            var address = _address.TryParseAndValidateAddress(accountService, hasErrorInput, true);
            var rpcAddress = _rpcAddress.TryParseRequiredString(hasErrorInput);
            var contratAddress = _contractAddress.TryParseAndValidateAddress(accountService, hasErrorInput, true);
            var numberOfDecimals = _numberOfDecimals.TryParseAndValidateInt(hasErrorInput, false);

            if (hasErrorInput) return 1;
            decimal balance = 0;
            if (numberOfDecimals == null)
            {
                balance = accountService.GetTokenBalanceAsync(address, contratAddress, rpcAddress).Result;
            }
            else
            {
                balance = accountService.GetTokenBalanceAsync(address, contratAddress, rpcAddress, numberOfDecimals.Value).Result;
            }

            System.Console.WriteLine("Total Balance: " + balance);

            return 0;
        }
    }
}