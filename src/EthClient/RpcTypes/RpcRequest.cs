using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Eth.RpcTypes
{
    public class RpcRequest : RpcMessage
    {
        [JsonProperty("method")]
        public string MethodName { get; set; }

        [JsonProperty("params")]
        public IEnumerable<object> Parameters { get; set; }

        public static IEnumerable<object> EmptyParameters { get { return Enumerable.Empty<object>(); } }
    }
}
