# Nethereum.Console

A collection of command line utilities to interact with Ethereum and account management

## Installation from Source

1. Install the .Net CLI (Windows, Mac, Linux) https://www.microsoft.com/net/download/core#/sdk
2. Clone directory / download source
3. Navigate to Nethereum.Console (directory containing Nethereum.Console.csproj)
4. Restore Dependencies

    ```dotnet restore```

5. Build

    ```dotnet build```

Once it is built you can run any command directly from the same directory

6. Run

    ```dotnet run create-account -p "superpassword" -dd "c:\Users\JuanFran\NewAccount" ```

# Commands

* [Create Account](#create-account)
* [Account or Accounts Total Balance](#account-or-accounts-total-balance)
* [Account or Accounts in Directory Total Balance](#account-or-accounts-in-directory-total-balance)
* [Account Transfer from Account File](#account-transfer-from-account-file)

There are more commands that listed here use ```dotnet Nethereum.Console.dll -h``` for more info.

## Create Account

Creates an account and stores it in a given folder

### Command
create-account

### Parameters

*  -p | --password            The password used for the account files
*  -dd | --destinationDirectory  Optional: The directory to create the account key store file
*  -? | -h | --help           Show help information

### Example
```
create-account -p "superpassword" -dd "c:\Users\JuanFran\NewAccount"
```
## Account or Accounts Total Balance
Calculates the total balance (in Ether) of an account or accounts using the address or addresses provided
### Command
accounts-total-balance

### Parameters

*  -a | --addr       The address or addresses to calculate the total balance
* -url              The rpc address to connect
* -? | -h | --help  Show help information

### Example
```
accounts-total-balance -url "https://mainnet.infura.io:8545" -a 0xb794f5ea0ba39494ce839613fffba74279579268 -a 0xe853c56864a2ebe4576a807d26fdc4a0ada51919
```

## Account or Accounts in Directory Total Balance
Calculates the total Ether balance of all the accounts in a given directory
### Command
accounts-dir-total-balance

### Parameters

* -sd | --sourceDirectory  The directory containing the source accounts
* -url                     The rpc address to connect
* -? | -h | --help         Show help information

### Example
```
accounts-dir-total-balance -url "https://mainnet.infura.io:8545" -sd "c:\Users\JuanFran\NewAccount"
```
## Account Transfer from Account File
Transfers ether from a given acccount file (key store file) to another account
### Command
account-transfer-from-account-file

### Parameters

 * -af | --accountFile  The account key store file
 * -ta | --toAddress    The address to send the ether to
 * -p | --password      The account file password
 * --url                The rpc address to connect
 * --amount             The amount in Ether to send
 * -? | -h | --help     Show help information

### Example
```
dotnet run account-transfer-from-account-file --url "http://localhost:8545" -af "C:\testchain\devChain\keystore\UTC--2015-11-25T05-05-03.116905600Z--12890d2cce102216644c59dae5baed380d84830c" -p "password" -ta 0x13f022d72158410433cbd66f5dd8bf6d2d129924 --amount 0.1
```

## Create accounts, mix and transfer
This is a security feature, for a quick and simple way to move of All the ether balances from a set of accounts to a newly created set of accounts. The amounts are transfered also randomly, achieving a "mix".

TODO: Add a delay function for transfers

### Command
create-acccounts-mix-balances

### Parameters

*  -sd | --sourceDirectory       The directory containing the source accounts
*  -dd | --destinationDirectory  The directory to create new accounts
*  -p | --password               The generic password used for all the account files
*  --url                         The rpc address to connect
*  -na                           Optional: The number of accounts to create, defaults to 4
*  -? | -h | --help              Show help information

# TODO
* SendTransaction (added for private key and key store file, this is done add readme usage)
* ERC20 balance (done, add readme usage), transfer
* Generare keystore file from private key, password
* Generate private key, address (done, todo add readme usage)
* Retrieve private key from key storage
* Message sign
* Deploy contract
* Generic Smart contract call / transfer using abi and method

# License

MIT

The MIT License (MIT)

Copyright (c) 2016 Nethereum.com (Juan Blanco) , Logo by Cass (https://github.com/cassiopaia)

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
