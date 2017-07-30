using System;
using Microsoft.Extensions.CommandLineUtils;

namespace Nethereum.Console
{
    public class TransferEtherFromAccountFileCommand : CommandLineApplication
    {
        private readonly CommandOption _accountFile;
        private readonly CommandOption _toAddress;
        private readonly CommandOption _password;
        private readonly CommandOption _rpcAddress;
        private readonly CommandOption _amount;


        public TransferEtherFromAccountFileCommand()
        {
            Name = "account-transfer-from-account-file";
            Description = "Transfers ether from a given acccount file (key store file) to another account";
            _accountFile = Option("-af | --accountFile", "The account key store file", CommandOptionType.SingleValue);
            _toAddress = Option("-ta | --toAddress", "The address to send the ether to", CommandOptionType.SingleValue);
            _password = Option("-p | --password", "The account file password", CommandOptionType.SingleValue);
            _rpcAddress = Option("--url", "The rpc address to connect", CommandOptionType.SingleValue);
            _amount = Option("--amount", "The amount in Ether to send", CommandOptionType.SingleValue);
            HelpOption("-? | -h | --help");
            OnExecute((Func<int>)RunCommand);
        }

        private int RunCommand()
        {
            IAccountService accountService = new AccountService();

            var accountFile = _accountFile.Value();
            if (string.IsNullOrWhiteSpace(accountFile))
            {
                System.Console.WriteLine("The account file was not specified");
                return 1;
            }

            var toAddress = _toAddress.Value();
            if (string.IsNullOrWhiteSpace(toAddress))
            {
                System.Console.WriteLine("The destination address was not specified");
                return 1;
            }

            var checkAddress = accountService.ValidAddressLength(toAddress);
            if (!checkAddress)
            {
                System.Console.WriteLine("The destination address should be 40 characters");
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

            decimal amount = 0;
            if (!string.IsNullOrWhiteSpace(_amount.Value()))
            {

                var passed = Decimal.TryParse(_amount.Value(), out amount);
                if (!passed)
                {
                    System.Console.WriteLine("Amount is not a valid decimal number");
                    return 1;
                }
            }
            else
            {
                System.Console.WriteLine("The amount was not specified");
                return 1;
            }

            var txn = accountService.TransferEther(accountFile, password, toAddress, amount, rpcAddress).Result;
            System.Console.WriteLine("Amount sent using transaction" + txn);
            return 0;
        }
    }
}