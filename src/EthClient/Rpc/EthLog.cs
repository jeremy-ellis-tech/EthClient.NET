using System.Collections.Generic;
using System.Numerics;

namespace Eth.Rpc
{
    public class EthLog
    {
        /// <summary>
        /// pending when the log is pending. mined if log is already mined.
        /// </summary>
        public EthLogType Type { get; set; }

        /// <summary>
        /// integer of the log index position in the block. null when its pending log.
        /// </summary>
        public BigInteger? LogIndex { get; set; }

        /// <summary>
        /// integer of the transactions index position log was created from. null when its pending log
        /// </summary>
        public BigInteger? TransactionIndex { get; set; }

        /// <summary>
        /// 32 Bytes - hash of the transactions this log was created from. null when its pending log
        /// </summary>
        public byte[] TransactionHash { get; set; }

        /// <summary>
        /// 32 Bytes - hash of the block where this log was in. null when its pending. null when its pending log.
        /// </summary>
        public byte[] BlockHash { get; set; }

        /// <summary>
        ///  the block number where this log was in. null when its pending. null when its pending log
        /// </summary>
        public BigInteger? BlockNumber { get; set; }

        /// <summary>
        /// 20 Bytes - address from which this log originated.
        /// </summary>
        public byte[] Address { get; set; }

        /// <summary>
        /// contains one or more 32 Bytes non-indexed arguments of the log.
        /// </summary>
        public byte[] Data { get; set; }

        /// <summary>
        /// Array of 0 to 4 32 Bytes DATA of indexed log arguments. (In solidity: The first topic is the hash of the signature of the event
        /// </summary>
        public IEnumerable<byte[]> Topics { get; set; }
    }
}
