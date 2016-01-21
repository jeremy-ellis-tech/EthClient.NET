using System.Numerics;

namespace Eth.RpcTypes
{
    public class EthTransaction
    {
        /// <summary>
        /// 20 Bytes - The address the transaction is sent from
        /// </summary>
        public byte[] From { get; set; }

        /// <summary>
        /// 20 Bytes - (optional when creating new contract) The address the transaction is directed to.
        /// </summary>
        public byte[] To { get; set; }

        /// <summary>
        /// (optional, default: 90000) Integer of the gas provided for the transaction execution.
        /// It will return unused gas.
        /// </summary>
        public BigInteger? Gas { get; set; }

        /// <summary>
        ///  (optional, default: To-Be-Determined) Integer of the gasPrice used for each paid gas
        /// </summary>
        public BigInteger? GasPrice { get; set; }

        /// <summary>
        ///  Integer of the value send with this transaction
        /// </summary>
        public BigInteger? Value { get; set; }

        /// <summary>
        /// The compiled code of a contract OR the hash of the invoked method signature and encoded parameters.
        /// </summary>
        public byte[] Data { get; set; }

        /// <summary>
        /// (optional) Integer of a nonce. This allows to overwrite your own pending transactions that use the same nonce.
        /// </summary>
        public BigInteger? Nonce { get; set; }
    }
}
