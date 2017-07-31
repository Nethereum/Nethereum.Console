using System;
using Microsoft.Extensions.CommandLineUtils;
using Nethereum.Hex.HexTypes;
using System.Numerics;

namespace Nethereum.Console
{
    public static class CommmandOptionExtensions
    {
        public static string TryParseRequiredString(this CommandOption option, bool hasInputErrors)
        {
            if (string.IsNullOrWhiteSpace(option.Value()))
            {
                    System.Console.WriteLine(option.ShortName + "|" + option.LongName + " has not been set");
                    hasInputErrors = true;
                    return null;
            }
            return option.Value();
        }

        public static string TryParseAndValidateAddress(this CommandOption option, IAccountService accountService, bool hasInputErrors, bool required = true)
        {
            var value = option.Value();
            if (required)
                value = TryParseRequiredString(option, hasInputErrors);

            if (!string.IsNullOrEmpty(value))
            {
                if (!accountService.ValidAddressLength(value))
                {
                    System.Console.WriteLine(option.ShortName + "|" + option.LongName + ": The address should have 40 characters in length");
                    hasInputErrors = true;
                }
                return value;
            }
            else
            {
                return null;
            }
        }

        public static decimal? TryParseAndValidateDecimal(this CommandOption option, bool hasInputErrors, bool required = true)
        {
            var value = option.Value();
            if (required)
                value = TryParseRequiredString(option, hasInputErrors);
            
            if (!string.IsNullOrEmpty(value))
            {
                decimal decimalvalue = 0;
                var passed = Decimal.TryParse(value, out decimalvalue);
                if (!passed)
                {
                    System.Console.WriteLine(option.ShortName + "|" + option.LongName + " is not a valid decimal");
                    return null;
                }
                return decimalvalue;
            }
            return null;
        }

        public static int? TryParseAndValidateInt(this CommandOption option, bool hasInputErrors, bool required = true)
        {
            var value = option.Value();
            if (required)
                value = TryParseRequiredString(option, hasInputErrors);

            if (!string.IsNullOrEmpty(value))
            {
                int intValue = 0;
                var passed = Int32.TryParse(value, out intValue);
                if (!passed)
                {
                    System.Console.WriteLine(option.ShortName + "|" + option.LongName + " is not a valid integer");
                    return null;
                }
                return intValue;
            }
            return null;
        }

        public static HexBigInteger TryParseHexBigIntegerValue(this CommandOption option, bool hasInputErrors, bool required = true)
        {
            var value = option.Value();
            if (required)
                value = TryParseRequiredString(option, hasInputErrors);

            HexBigInteger val = null;
            BigInteger biValue = 0;

            if (!string.IsNullOrWhiteSpace(option.Value()))
            {
                var passed = BigInteger.TryParse(option.Value(), out biValue);
                if (!passed)
                {
                    System.Console.WriteLine(option.ShortName + "|" + option.LongName + " is not a valid BigInteger");
                    hasInputErrors = true;
                }
                else
                {
                    return new HexBigInteger(biValue);
                }
            }

            return null;
        }

    }
}