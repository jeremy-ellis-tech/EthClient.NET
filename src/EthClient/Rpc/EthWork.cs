using System.Linq;

namespace Eth.Rpc
{
    public class EthWork
    {
        /// <summary>
        /// current block header pow-hash
        /// </summary>
        public byte[] BlockHash { get; set; }

        /// <summary>
        /// the seed hash used for the DAG.
        /// </summary>
        public byte[] SeedHash { get; set; }

        /// <summary>
        /// the boundary condition ("target"), 2^256 / difficulty.
        /// </summary>
        public byte[] BoundaryCondition { get; set; }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(obj, this)) return true;

            var other = obj as EthWork;

            if (other == null) return false;

            return BlockHash.SequenceEqual(other.BlockHash)
                && SeedHash.SequenceEqual(other.SeedHash)
                && BoundaryCondition.SequenceEqual(other.BoundaryCondition);
        }

        public override int GetHashCode()
        {
            return BlockHash.GetHashCode() + SeedHash.GetHashCode() + BoundaryCondition.GetHashCode();
        }

        public override string ToString()
        {
            return "EthWork";
        }
    }
}
