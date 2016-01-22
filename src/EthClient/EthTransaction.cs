using System;
using System.Numerics;

namespace Eth
{
    public class EthTransaction
    {
        /// <summary>
        /// An Ethereum Transaction
        /// </summary>
        /// <param name="from">20 Bytes - The address the transaction is sent from</param>
        /// <param name="data">The compiled code of a contract OR the hash of the invoked method signature and encoded parameters.</param>
        /// <param name="to">20 Bytes - (optional when creating new contract) The address the transaction is directed to.</param>
        /// <param name="gas">(optional, default: 90000) Integer of the gas provided for the transaction execution. It will return unused gas.</param>
        /// <param name="gasPrice">(optional, default: To-Be-Determined) Integer of the gasPrice used for each paid gas</param>
        /// <param name="value">Integer of the value send with this transaction</param>
        /// <param name="nonce">(optional) Integer of a nonce. This allows to overwrite your own pending transactions that use the same nonce.</param>
        public EthTransaction(byte[] from, byte[] data, byte[] to = null, BigInteger? gas = null, BigInteger? gasPrice = null, BigInteger? value = null, BigInteger? nonce = null)
        {
            if(from == null)
            {
                throw new ArgumentNullException("from");
            }

            if(data == null)
            {
                throw new ArgumentNullException("data");
            }

            if(from.Length != 20)
            {
                throw new ArgumentOutOfRangeException("from");
            }

            if(to != null && to.Length != 20)
            {
                throw new ArgumentOutOfRangeException("to");
            }

            From = from;
            To = to;
            Gas = gas;
            GasPrice = gasPrice;
            Value = value;
            Data = data;
            Nonce = nonce;
        }

        public byte[] From { get; private set; }

        public byte[] To { get; private set; }

        public BigInteger? Gas { get; private set; }

        public BigInteger? GasPrice { get; private set; }

        public BigInteger? Value { get; private set; }

        public byte[] Data { get; private set; }

        public BigInteger? Nonce { get; private set; }
    }
}
