using System.Numerics;

namespace Eth
{
    public class EthCall
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
        /// (optional) Integer of the gas provided for the transaction execution.
        /// eth_call consumes zero gas, but this parameter may be needed by some executions.
        /// </summary>
        public BigInteger? Gas { get; set; }

        /// <summary>
        ///  (optional) Integer of the gasPrice used for each paid gas
        /// </summary>
        public BigInteger? GasPrice { get; set; }

        /// <summary>
        /// (optional) Integer of the value send with this transaction
        /// </summary>
        public BigInteger? Value { get; set; }

        /// <summary>
        /// (optional) Hash of the method signature and encoded parameters
        /// </summary>
        public byte[] Data { get; set; }
    }
}
