using System;
using System.Numerics;

namespace Eth
{
    public class EthSyncing
    {
        public EthSyncing(BigInteger startingBlock, BigInteger currentBlock, BigInteger highestBlock)
        {
            IsSynching = true;
            StartingBlock = startingBlock;
            CurrentBlock = currentBlock;
            HighestBlock = highestBlock;
        }

        public EthSyncing(bool isSynching = false)
        {
            IsSynching = isSynching;
        }

        public bool IsSynching { get; private set; }

        public BigInteger? StartingBlock { get; private set; }

        public BigInteger? CurrentBlock { get; private set; }

        public BigInteger? HighestBlock { get; private set; }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(obj, this)) return true;

            var other = obj as EthSyncing;

            if (other == null) return false;

            return Equals(other.IsSynching, IsSynching)
                && Equals(other.CurrentBlock, CurrentBlock)
                && Equals(other.HighestBlock, HighestBlock)
                && Equals(other.StartingBlock, StartingBlock);
        }

        public override int GetHashCode()
        {
            if (!IsSynching)
            {
                return IsSynching.GetHashCode();
            }
            else
            {
                return StartingBlock.GetHashCode() + CurrentBlock.GetHashCode() + HighestBlock.GetHashCode();
            }
        }

        public override string ToString()
        {
            return String.Format("EthSyncing - IsSyncing: {0}", IsSynching);
        }
    }
}
