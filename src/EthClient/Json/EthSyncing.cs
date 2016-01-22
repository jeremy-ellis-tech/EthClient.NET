using Newtonsoft.Json;

namespace Eth.Json
{
    public class EthSyncing
    {
        [JsonProperty("startingBlock")]
        public string StartingBlock { get; set; }

        [JsonProperty("currentBlock")]
        public string CurrentBlock { get; set; }

        [JsonProperty("highestBlock")]
        public string HighestBlock { get; set; }
    }
}
