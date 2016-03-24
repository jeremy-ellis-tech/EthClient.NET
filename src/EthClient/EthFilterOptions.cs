using System.Collections.Generic;

namespace Eth
{
    public class EthFilterOptions
    {
        /// <summary>
        /// Integer block number, or "latest" for the last mined block or "pending", "earliest" for not yet mined transactions.
        /// </summary>
        public DefaultBlock FromBlock { get; set; }

        /// <summary>
        /// Integer block number, or "latest" for the last mined block or "pending", "earliest" for not yet mined transactions.
        /// </summary>
        public DefaultBlock ToBlock { get; set; }

        /// <summary>
        /// 20 Bytes - (optional) Contract address or a list of addresses from which logs should originate
        /// </summary>
        public IEnumerable<byte[]> Address { get; set; }

        /// <summary>
        /// Array of 32 Bytes DATA topics. Topics are order-dependent. Each topic can also be an array of DATA with "or" options.
        /// </summary>
        public IList<EthTopic> Topics { get; set; }
    }
}
