using System.Collections.Generic;

namespace Eth
{
    public class RpcRequest : RpcMessage
    {
        public string MethodName { get; set; }
        public IEnumerable<object> Parameters { get; set; }
    }
}
