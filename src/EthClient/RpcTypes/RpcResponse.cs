using Newtonsoft.Json;

namespace Eth.RpcTypes
{
    public class RpcResponse<T> : RpcMessage
    {
        [JsonProperty("result")]
        public T Result { get; set; }
    }
}
