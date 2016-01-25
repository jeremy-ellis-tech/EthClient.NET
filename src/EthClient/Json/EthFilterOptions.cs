using Newtonsoft.Json;
using System.Collections.Generic;

namespace Eth.Json
{
    public class EthFilterOptions
    {
        [JsonProperty("fromBlock")]
        public string FromBlock { get; set; }

        [JsonProperty("toBlock")]
        public string ToBlock { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("topics")]
        public List<object> Topics { get; set; }
    }
}
