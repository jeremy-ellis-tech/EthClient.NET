using System.Numerics;

namespace Eth
{
    public class EthSyncing
    {
        internal EthSyncing(BigInteger startingBlock, BigInteger currentBlock, BigInteger highestBlock)
        {
            IsSynching = true;
            StartingBlock = startingBlock;
            CurrentBlock = currentBlock;
            HighestBlock = highestBlock;
        }

        internal EthSyncing(bool isSynching)
        {
            IsSynching = isSynching;
        }

        public bool IsSynching { get; private set; }

        public BigInteger? StartingBlock { get; private set; }

        public BigInteger? CurrentBlock { get; private set; }

        public BigInteger? HighestBlock { get; private set; }
    }
}
