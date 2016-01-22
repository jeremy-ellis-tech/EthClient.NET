using Newtonsoft.Json;

namespace Eth.Json
{
    public abstract class RpcMessage
    {
        [JsonProperty("id")]
        public int ID { get; set; }

        [JsonProperty("jsonrpc")]
        public string JsonRpc { get; set; }
    }
}
