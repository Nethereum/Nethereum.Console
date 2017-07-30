using Nethereum.Web3.Accounts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nethereum.Console
{
    public interface IAccountService
    {
        Account CreateAccount(string password, string path);
        Account LoadFromKeyStoreFile(string filePath, string password);
        Task<decimal> CalculateTotalBalanceAccountsInFolder(string rpcAddress, string folder);
        Task<decimal> CalculateTotalBalanceAccounts(string rpcAddress, List<string> addresses);
        bool ValidAddressLength(string address);
        Task<string> TransferEther(string keyStoreFilePath, string keyStorePassword, string addressTo, decimal etherAmount, string rpcUrl);
    }
}