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

        internal DefaultBlock(DefaultBlockOption option)
        {
            Option = option;
        }

        public BigInteger? BlockNumber { get; private set; }

        public DefaultBlockOption? Option { get; private set; }

        public static DefaultBlock Earliest
        {
            get
            {
                return new DefaultBlock(DefaultBlockOption.Earliest);
            }
        }

        public static DefaultBlock Latest
        {
            get
            {
                return new DefaultBlock(DefaultBlockOption.Latest);
            }
        }

        public static DefaultBlock Pending
        {
            get
            {
                return new DefaultBlock(DefaultBlockOption.Pending);
            }
        }
    }

    public enum DefaultBlockOption
    {
        Earliest,
        Latest,
        Pending
    }
}
