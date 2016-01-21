using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eth.RpcTypes
{
    public class EthWork
    {
        internal EthWork(byte[] blockHash, byte[] seedHash, byte[]boundaryCondition)
        {
            BlockHash = blockHash;
            SeedHash = seedHash;
            BoundaryCondition = boundaryCondition;
        }

        public byte[] BlockHash { get; private set; }

        public byte[] SeedHash { get; private set; }

        public byte[] BoundaryCondition { get; private set; }
    }
}
