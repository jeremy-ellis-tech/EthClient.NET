using Eth.Utilities;
using System.Collections.Generic;

namespace Eth
{
    public class EthFilterOptions
    {
        /// <summary>
        /// Filter options
        /// </summary>
        /// <param name="fromBlock"> Integer block number, or "latest" for the last mined block or "pending", "earliest" for not yet mined transactions. Default 'latest'</param>
        /// <param name="toBlock">Integer block number, or "latest" for the last mined block or "pending", "earliest" for not yet mined transactions. Default 'latest'</param>
        /// <param name="address">20 Bytes - (optional) Contract address or a list of addresses from which logs should originate</param>
        /// <param name="topics">(optional) Array of 32 Bytes DATA topics. Topics are order-dependent. Each topic can also be an array of DATA with "or" options</param>
        public EthFilterOptions(DefaultBlock fromBlock = null, DefaultBlock toBlock = null, ICollection<byte[]> address = null, IList<object> topics = null)
        {
            FromBlock = fromBlock;
            ToBlock = toBlock;

            if(address != null)
            {
                foreach (var adr in address)
                {
                    Ensure.EnsureCountIsCorrect(adr, EthSpecs.AddressLength, "address");
                }
            }

            Topics = topics;
        }

        public DefaultBlock FromBlock { get; private set; }

        public DefaultBlock ToBlock { get; private set; }

        public ICollection<byte[]> Address { get; private set; }

        public IList<object> Topics { get; private set; }
    }
}
