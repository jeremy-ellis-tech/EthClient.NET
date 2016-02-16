using System.Collections.Generic;
using System.Numerics;

namespace Eth
{
    public class EthTransactionReceipt
    {
        public byte[] TransactionHash { get; set; }

        public BigInteger TransactionIndex { get; set; }

        public BigInteger BlockNumber { get; set; }

        public byte[] BlockHash { get; set; }

        public BigInteger CumulativeGasUsed { get; set; }

        public BigInteger GasUsed { get; set; }

        public byte[] ContractAddress { get; set; }

        public IEnumerable<Json.EthLog> Logs { get; set; }
    }
}
