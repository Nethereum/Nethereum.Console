using Nethereum.KeyStore;
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
            using (var file = File.OpenText(filePath))
            {
                var json = file.ReadToEnd();
                var key = keyStoreService.DecryptKeyStoreFromJson(password, json);
                return new Account(key);
            }
        }

        public async Task<Decimal> CalculateTotalBalanceAccountsInFolder(string rpcAddress, string folder)
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
 
            return await CalculateTotalBalanceAccounts(rpcAddress, addresses).ConfigureAwait(false);
        }

        public async Task<Decimal> CalculateTotalBalanceAccounts(string rpcAddress, List<string> addresses)
        {
            BigInteger balance = 0;
            Web3.Web3 web3 = new Web3.Web3(rpcAddress);

            foreach (var address in addresses)
            {
               balance = balance + await web3.Eth.GetBalance.SendRequestAsync(address).ConfigureAwait(false);   
            }

            return Web3.Web3.Convert.FromWei(balance);
        }
    }
}
