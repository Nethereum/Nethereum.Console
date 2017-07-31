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
        Task<decimal> CalculateTotalBalanceAccountsInFolderAsync(string rpcAddress, string folder);
        Task<decimal> CalculateTotalBalanceAccountsAsync(string rpcAddress, List<string> addresses);
        bool ValidAddressLength(string address);
        Task<string> TransferEtherAsync(string keyStoreFilePath, string keyStorePassword, string addressTo, decimal etherAmount, string rpcUrl);
        Task<string> TransferEtherAsync(Account account, string addressTo, decimal etherAmount, string rpcUrl);
        Task<string> SendTransactionAsync(string keyStoreFilePath, string keyStorePassword, string addressTo, decimal etherAmount, string rpcUrl, HexBigInteger gas, HexBigInteger gasPrice, string data);
        Task<string> SendTransactionAsync(Account account, string addressTo, decimal etherAmount, string rpcUrl, HexBigInteger gas, HexBigInteger gasPrice, string data);
        Task<decimal> GetTokenBalanceAsync(string adddress, string contractAddress, string rpcUrl, int numberOfDecimalPlaces = 18);
    }
}