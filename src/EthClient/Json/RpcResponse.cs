using Newtonsoft.Json;

namespace Eth.Json
{
    public class RpcResponse<T> : RpcMessage
    {
        [JsonProperty("result")]
        public T Result { get; set; }
    }
}
