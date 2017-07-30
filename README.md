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

## Create accounts, mix and transfer