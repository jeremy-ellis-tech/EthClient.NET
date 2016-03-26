using Eth.Utilities;
using System.Numerics;

namespace Eth.Rpc
{
    public class EthTransaction
    {
        public byte[] From { get; set; }

        public byte[] To { get; set; }

        public BigInteger? Gas { get; set; }

        public BigInteger? GasPrice { get; set; }

        public BigInteger? Value { get; set; }

        public byte[] Data { get; set; }

        public BigInteger? Nonce { get; set; }
    }
}
