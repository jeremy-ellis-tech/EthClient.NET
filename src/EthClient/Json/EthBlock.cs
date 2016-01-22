using Newtonsoft.Json;
using System.Collections.Generic;

namespace Eth.Json
{
    /// <summary>
    /// JSON type returned by EthGetBlockBy* calls.
    /// </summary>
    public class EthBlock
    {
        [JsonProperty("number")]
        public string Number { get; set; }

        [JsonProperty("hash")]
        public string Hash { get; set; }

        [JsonProperty("parentHash")]
        public string ParentHash { get; set; }

        [JsonProperty("nonce")]
        public string Nonce { get; set; }

        [JsonProperty("sha3uncles")]
        public string Sha3Uncles { get; set; }

        [JsonProperty("logsBloom")]
        public string LogsBloom { get; set; }

        [JsonProperty("transactionRoot")]
        public string TransactionRoot { get; set; }

        [JsonProperty("stateRoot")]
        public string StateRoot { get; set; }

        [JsonProperty("receiptsRoot")]
        public string ReceiptsRoot { get; set; }

        [JsonProperty("miner")]
        public string Miner { get; set; }

        [JsonProperty("difficulty")]
        public string Difficulty { get; set; }

        [JsonProperty("totalDifficulty")]
        public string TotalDifficulty { get; set; }

        [JsonProperty("extraData")]
        public string ExtraData { get; set; }

        [JsonProperty("size")]
        public string Size { get; set; }

        [JsonProperty("gasLimit")]
        public string GasLimit { get; set; }

        [JsonProperty("gasUsed")]
        public string GasUsed { get; set; }

        [JsonProperty("timestamp")]
        public string TimeStamp { get; set; }

        [JsonProperty("transactions")]
        public IEnumerable<string> Transactions { get; set; }

        [JsonProperty("uncles")]
        public IEnumerable<string> Unlces { get; set; }
    }
}
