using System;
using System.Numerics;

namespace Eth
{
    public class DefaultBlock
    {
        public DefaultBlock(BigInteger blockNumber)
        {
            if(blockNumber < BigInteger.Zero)
            {
                throw new ArgumentOutOfRangeException("blockNumber");
            }

            BlockNumber = blockNumber;
        }

        public DefaultBlock(DefaultBlockParameterOption option)
        {
            Option = option;
        }

        public BigInteger? BlockNumber { get; private set; }

        public DefaultBlockParameterOption? Option { get; private set; }
    }

    public enum DefaultBlockParameterOption
    {
        Earliest,
        Latest,
        Pending
    }
}
