using System.Collections.Generic;
using System.Numerics;

namespace Eth.Rpc
{
    public class ShhPost
    {
        /// <summary>
        /// 60 Bytes - (optional) The identity of the sender.
        /// </summary>
        public byte[] From { get; set; }

        /// <summary>
        /// 60 Bytes - (optional) The identity of the receiver.
        /// When present whisper will encrypt the message so
        /// that only the receiver can decrypt it.
        /// </summary>
        public byte[] To { get; set; }

        /// <summary>
        /// Array of DATA topics, for the receiver to identify messages.
        /// </summary>
        public IList<EthTopic> Topics { get; set; }

        /// <summary>
        /// The payload of the message.
        /// </summary>
        public byte[] Payload { get; set; }

        /// <summary>
        /// The integer of the priority in a rang from ... (?).
        /// </summary>
        public BigInteger Priority { get; set; }

        /// <summary>
        /// integer of the time to live in seconds.
        /// </summary>
        public BigInteger Ttl { get; set; }
    }
}