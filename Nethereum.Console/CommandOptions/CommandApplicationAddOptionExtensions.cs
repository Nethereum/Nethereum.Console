using Microsoft.Extensions.CommandLineUtils;

namespace Nethereum.Console
{
    public static class CommandApplicationAddOptionExtensions
    {
        public static CommandOption AddOptionAccoutFilePassword(this CommandLineApplication commmandLineApplication)
        {
            return commmandLineApplication.Option("-p | --password", "The account file password", CommandOptionType.SingleValue);
        }

        public static CommandOption AddOptionPrivateKey(this CommandLineApplication commmandLineApplication)
        {
            return commmandLineApplication.Option("-pk | --privateKey", "The private key", CommandOptionType.SingleValue);
        }

        public static CommandOption AddOptionAccoutKeyStoreFile(this CommandLineApplication commmandLineApplication)
        {
            return commmandLineApplication.Option("-af | --accountFile", "The account key store file", CommandOptionType.SingleValue);
        }

        public static CommandOption AddOptionAmountInEther(this CommandLineApplication commmandLineApplication, bool required = true)
        { 
            return commmandLineApplication.Option("--amount", Optional(required) + "The amount in Ether to send", CommandOptionType.SingleValue);
        }

        public static CommandOption AddOptionDestinationAddress(this CommandLineApplication commmandLineApplication, bool required = true)
        {
            return commmandLineApplication.Option("-ta | --toAddress", Optional(required) + "The destination address", CommandOptionType.SingleValue);
        }

        public static CommandOption AddOptionGas(this CommandLineApplication commmandLineApplication, bool required = false)
        {
            return commmandLineApplication.Option("-g | --gas", Optional(required) + "The max gas to be used by the transaction", CommandOptionType.SingleValue);
        }

        public static CommandOption AddOptionGasPrice(this CommandLineApplication commmandLineApplication, bool required = false)
        {
            return commmandLineApplication.Option("-gp | --gasPrice", Optional(required) + "The gas price", CommandOptionType.SingleValue);
        }

        public static CommandOption AddOptionData(this CommandLineApplication commmandLineApplication, bool required = false)
        {
            return commmandLineApplication.Option("--data", Optional(required) + "The data to be sent in the transaction", CommandOptionType.SingleValue);
        }

        public static CommandOption AddOptionRpcAddress(this CommandLineApplication commmandLineApplication)
        {
            return commmandLineApplication.Option("--url", "The rpc address to connect", CommandOptionType.SingleValue);
        }

        public static string Optional(bool required)
        {
            return  !required ? "Optional" : "";
        }
    }
}