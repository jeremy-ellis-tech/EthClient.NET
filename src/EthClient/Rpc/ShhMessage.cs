using System;
using System.Collections.Generic;
using System.Numerics;

namespace Eth.Rpc
{
    public class ShhMessage
    {
        /// <summary>
        /// 32 Bytes (?) - The hash of the message.
        /// </summary>
        public byte[] Hash { get; set; }

        /// <summary>
        /// 60 Bytes - The sender of the message, if a sender was specified.
        /// </summary>
        public byte[] From { get; set; }

        /// <summary>
        /// 60 Bytes - The receiver of the message, if a receiver was specified.
        /// </summary>
        public byte[] To { get; set; }

        /// <summary>
        /// Integer of the time in seconds when this message should expire (?).
        /// </summary>
        public BigInteger Expiry { get; set; }

        /// <summary>
        /// Time the message should float in the system.
        /// </summary>
        public TimeSpan Ttl { get; set; }

        /// <summary>
        /// timestamp when the message was sent.
        /// </summary>
        public DateTimeOffset Sent { get; set; }

        /// <summary>
        /// Array of DATA topics the message contained.
        /// </summary>
        public IList<EthTopic> Topics { get; set; }

        /// <summary>
        /// The payload of the message.
        /// </summary>
        public byte[] Payload { get; set; }

        /// <summary>
        /// Integer of the work this message required before it was send (?).
        /// </summary>
        public BigInteger WorkProved { get; set; }
    }
}