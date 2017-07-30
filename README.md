# Nethereum.Console

A collection of command line utilities to interact with Ethereum and account management

## Create Account

## Acounts in Folder Total Balance

## Accounts Total Balance
Calculates the total balance (in Ether) of an account or accounts using the addresses provided
### Method
accounts-total-balance

### Parameters
  -a | --addr       The address or addresses to calculate the total balance
  -url              The rpc address to connect
  -? | -h | --help  Show help information

### Example
```
accounts-total-balance -url "https://mainnet.infura.io:8545" -a 0xb794f5ea0ba39494ce839613fffba74279579268 -a 0xe853c56864a2ebe4576a807d26fdc4a0ada51919
```

## Create accounts, mix and transfer