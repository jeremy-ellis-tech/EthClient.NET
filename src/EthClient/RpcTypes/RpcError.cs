using Newtonsoft.Json;

namespace Eth.RpcTypes
{
    public class RpcError : RpcMessage
    {
        [JsonProperty("error")]
        public Error Error { get; set; }
    }

    public class Error
    {
        [JsonProperty("code")]
        public int ErrorCode { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
