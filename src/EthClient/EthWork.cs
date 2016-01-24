using Eth.Utilities;

namespace Eth
{
    public class EthWork
    {
        internal EthWork(byte[] blockHash, byte[] seedHash, byte[]boundaryCondition)
        {
            Ensure.EnsureParameterIsNotNull(blockHash, "blockHash");
            Ensure.EnsureParameterIsNotNull(seedHash, "seedHash");
            Ensure.EnsureParameterIsNotNull(boundaryCondition, "boundaryCondition");

            Ensure.EnsureCountIsCorrect(blockHash, EthSpecs.BlockHashLength, "blockHash");
            Ensure.EnsureCountIsCorrect(seedHash, EthSpecs.SeedHashLength, "seedHash");
            Ensure.EnsureCountIsCorrect(boundaryCondition, EthSpecs.BoundaryConditionLength, "boundaryCondition");

            BlockHash = blockHash;
            SeedHash = seedHash;
            BoundaryCondition = boundaryCondition;
        }

        public byte[] BlockHash { get; private set; }

        public byte[] SeedHash { get; private set; }

        public byte[] BoundaryCondition { get; private set; }
    }
}
