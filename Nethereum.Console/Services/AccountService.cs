using Nethereum.Hex.HexConvertors.Extensions;
using Nethereum.Hex.HexTypes;
using Nethereum.KeyStore;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Web3.Accounts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Nethereum.Console
{
    public class AccountService : IAccountService
    {
        public Account CreateAccount(string password, string path)
        {
            if (!Directory.Exists(path)){
                Directory.CreateDirectory(path);
            }
            //Generate a private key pair using SecureRandom
            var ecKey = Nethereum.Signer.EthECKey.GenerateKey();
            //Get the public address (derivied from the public key)
            var address = ecKey.GetPublicAddress();

            //Create a store service, to encrypt and save the file using the web3 standard
            var service = new KeyStoreService();
            var encryptedKey = service.EncryptAndGenerateDefaultKeyStoreAsJson(password, ecKey.GetPrivateKeyAsBytes(), address);
            var fileName = service.GenerateUTCFileName(address);
            //save the File
            using (var newfile = File.CreateText(Path.Combine(path, fileName)))
            {
                newfile.Write(encryptedKey);
                newfile.Flush();
            }

            return new Account(ecKey.GetPrivateKey());
        }

        public Account LoadFromKeyStoreFile(string filePath, string password)
        {
            var keyStoreService = new KeyStoreService();

            if (!File.Exists(filePath)) throw new Exception("Account keystore file not found");

            using (var file = File.OpenText(filePath))
            {
                var json = file.ReadToEnd();
                var key = keyStoreService.DecryptKeyStoreFromJson(password, json);
                return new Account(key);
            }
        }

        public async Task<Decimal> CalculateTotalBalanceAccountsInFolderAsync(string rpcAddress, string folder)
        {
            var addresses = new List<string>();
            foreach (var file in Directory.GetFiles(folder))
            {
                var service = new KeyStore.KeyStoreService();
                using (var jsonFile = File.OpenText(file))
                {
                    var json = jsonFile.ReadToEnd();
                    var address = service.GetAddressFromKeyStore(json);
                    addresses.Add(address);
                }
            }

            return await CalculateTotalBalanceAccountsAsync(rpcAddress, addresses).ConfigureAwait(false);
        }

        public async Task<Decimal> CalculateTotalBalanceAccountsAsync(string rpcAddress, List<string> addresses)
        {
            BigInteger balance = 0;
            Web3.Web3 web3 = new Web3.Web3(rpcAddress);

            foreach (var address in addresses)
            {
               balance = balance + await web3.Eth.GetBalance.SendRequestAsync(address).ConfigureAwait(false);   
            }

            return Web3.Web3.Convert.FromWei(balance);
        }

        public bool ValidAddressLength(string address)
        {
            var checkAddress = address.RemoveHexPrefix();
            return checkAddress.Length == 40;
        }

        public void CheckAddressLengthAndThrow(string address)
        {
            if (!ValidAddressLength(address)) throw new Exception("Invalid address length, should be 40 characters");
        }

        public async Task<string> TransferEtherAsync(Account account, string addressTo, decimal etherAmount, string rpcUrl)
        {
            return await SendTransactionAsync(account, addressTo, etherAmount, rpcUrl, null, null, null).ConfigureAwait(false);
        }

        public async Task<string> TransferEtherAsync(string keyStoreFilePath, string keyStorePassword, string addressTo, decimal etherAmount, string rpcUrl)
        {
            return await SendTransactionAsync(keyStoreFilePath, keyStorePassword, addressTo, etherAmount, rpcUrl, null, null, null).ConfigureAwait(false);
        }

        public async Task<string> SendTransactionAsync(string keyStoreFilePath, string keyStorePassword, string addressTo, decimal etherAmount, string rpcUrl, HexBigInteger gas, HexBigInteger gasPrice, string data)
        {
            var account = LoadFromKeyStoreFile(keyStoreFilePath, keyStorePassword);
            return await SendTransactionAsync(account, addressTo, etherAmount, rpcUrl, gas, gasPrice, data);
        }

        public async Task<string> SendTransactionAsync(Account account, string addressTo, decimal etherAmount, string rpcUrl, HexBigInteger gas, HexBigInteger gasPrice, string data)
        {
            CheckAddressLengthAndThrow(addressTo);
            BigInteger weiAmount = 0;
            if (etherAmount > 0)
            {
                weiAmount = Web3.Web3.Convert.ToWei(etherAmount);
            }
            var web3 = new Web3.Web3(account, rpcUrl);
            var transactionInput = new TransactionInput()
            {
                From = account.Address,
                To = addressTo,
                Value = new HexBigInteger(weiAmount),
                Data = data,
                Gas = gas,
                GasPrice = gasPrice
            };

            var txn = await web3.Eth.TransactionManager.SendTransactionAsync(transactionInput).ConfigureAwait(false);
            return txn;
        }

        public async Task<decimal> GetTokenBalanceAsync(string adddress, string contractAddress, string rpcUrl, int numberOfDecimalPlaces = 18)
        {
            var service = new StandardTokenEIP20.StandardTokenService(new Web3.Web3(rpcUrl), contractAddress);
            var balance = await service.GetBalanceOfAsync<BigInteger>(adddress);
            return Web3.Web3.Convert.FromWei(balance, numberOfDecimalPlaces);
        }


    }
}
