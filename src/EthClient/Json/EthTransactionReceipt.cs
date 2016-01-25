using Newtonsoft.Json;
using System.Collections.Generic;

namespace Eth.Json
{
    public class EthTransactionReceipt
    {
        [JsonProperty("transactionHash")]
        public string TransactionHash { get; set; }

        [JsonProperty("transactionIndex")]
        public string TransactionIndex { get; set; }

        [JsonProperty("blockNumber")]
        public string BlockNumber { get; set; }

        [JsonProperty("blockHash")]
        public string BlockHash { get; set; }

        [JsonProperty("cumulativeGasUsed")]
        public string CumulativeGasUsed { get; set; }

        [JsonProperty("gasUsed")]
        public string GasUsed { get; set; }

        [JsonProperty("contractAddress")]
        public string ContractAddress { get; set; }

        [JsonProperty("logs")]
        public IEnumerable<EthLog> Logs { get; set; }
    }
}
