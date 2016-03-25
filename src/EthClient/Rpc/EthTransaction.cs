using Eth.Utilities;
using System.Numerics;

namespace Eth.Rpc
{
    public class EthTransaction
    {
        public byte[] From { get; private set; }

        public byte[] To { get; private set; }

        public BigInteger? Gas { get; private set; }

        public BigInteger? GasPrice { get; private set; }

        public BigInteger? Value { get; private set; }

        public byte[] Data { get; private set; }

        public BigInteger? Nonce { get; private set; }
    }
}
