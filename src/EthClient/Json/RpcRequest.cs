using Newtonsoft.Json;
using System.Collections.Generic;

namespace Eth.Json
{
    public class RpcRequest : RpcMessage
    {
        [JsonProperty("method")]
        public string MethodName { get; set; }

        [JsonProperty("params")]
        public IEnumerable<object> Parameters { get; set; }
    }
}
