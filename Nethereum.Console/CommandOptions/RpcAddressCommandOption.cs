using Microsoft.Extensions.CommandLineUtils;

namespace Nethereum.Console
{
    public class RpcAddressCommandOption : ICommandOption
    {
        public CommandOption RpcAddressOption { get; set; }
        public string RpcAddress { get; set; }
        public bool HasInputErrors { get; protected set;}

        public virtual void AddOptionToCommandLineApplication(CommandLineApplication commandLineApplication)
        {
            RpcAddressOption = commandLineApplication.AddOptionRpcAddress();
        }

        public virtual void ParseAndValidateInput()
        {
            RpcAddress = RpcAddressOption.TryParseRequiredString(HasInputErrors);
        }
    }
}