using Nethereum.Web3.Accounts;
using System.Collections.Generic;
using System.Threading.Tasks;
using Nethereum.Hex.HexTypes;

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
        Task<string> TransferEther(Account account, string addressTo, decimal etherAmount, string rpcUrl);
        Task<string> SendTransaction(string keyStoreFilePath, string keyStorePassword, string addressTo, decimal etherAmount, string rpcUrl, HexBigInteger gas, HexBigInteger gasPrice, string data);
        Task<string> SendTransaction(Account account, string addressTo, decimal etherAmount, string rpcUrl, HexBigInteger gas, HexBigInteger gasPrice, string data);

    }
}