using System.Collections.Generic;

namespace Eth.Rpc
{
    public class ShhFilterOptions
    {
        /// <summary>
        /// 60 Bytes - (optional) Identity of the receiver.
        /// When present it will try to decrypt any incoming
        /// message if the client holds the private key to
        /// this identity.
        /// </summary>
        public byte[] To { get; set; }

        /// <summary>
        /// Array of DATA topics which the incoming message's
        /// topics should match
        /// </summary>
        public IList<EthTopic> Topics { get; set; }
    }
}