using System;
using System.Numerics;

namespace Eth.Utilities
{
    public static class EthCurrencyConverter
    {
        /// <summary>
        /// Convert a currency value from it's value in Wei.
        /// </summary>
        /// <param name="value">The amount</param>
        /// <param name="unit">The currency unit to return</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if value is less than zero</exception>
        /// <exception cref="OverflowException">Thrown if answer cannot be represented by a decimal type</exception>
        /// <returns></returns>
        public static decimal FromWei(BigInteger value, EthCurrencyUnit unit)
        {
            if(value < BigInteger.Zero)
            {
                throw new ArgumentOutOfRangeException("value");
            }

            checked
            {
                return (decimal)value / GetWeiMultiplier(unit);
            }
        }

        /// <summary>
        /// Convert a currency value to it's value in wei.
        /// </summary>
        /// <param name="value">The amount</param>
        /// <param name="unit">The currency unit the value is in</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if value is less than zero</exception>
        /// <exception cref="OverflowException">Thrown if value * wei multipiler is greater than Decimal.MaxValue</exception>
        /// <returns></returns>
        public static BigInteger ToWei(decimal value, EthCurrencyUnit unit)
        {
            if(value < Decimal.Zero)
            {
                throw new ArgumentOutOfRangeException("value");
            }

            checked
            {
                return (BigInteger)(value * GetWeiMultiplier(unit));
            }
        }

        private static long GetWeiMultiplier(EthCurrencyUnit unit)
        {
            switch (unit)
            {
                case EthCurrencyUnit.Wei:
                    //10^0
                    return 1;
                case EthCurrencyUnit.Szabo:
                    //10^12
                    return 1000000000000;
                case EthCurrencyUnit.Finney:
                    //10^15
                    return 1000000000000000;
                case EthCurrencyUnit.Ether:
                    //10^18
                    return 1000000000000000000;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
