using Newtonsoft.Json;
using System.Collections.Generic;

namespace Eth.Json
{
    public class EthLog
    {
        [JsonProperty("logIndex")]
        public string LogIndex { get; set; }

        [JsonProperty("blockNumber")]
        public string BlockNumber { get; set; }

        [JsonProperty("blockHash")]
        public string BlockHash { get; set; }

        [JsonProperty("transactionHash")]
        public string TransactionHash { get; set; }

        [JsonProperty("transactionIndex")]
        public string TransactionIndex { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("data")]
        public string Data { get; set; }

        [JsonProperty("topics")]
        public IEnumerable<string> Topics { get; set; }
    }
}
