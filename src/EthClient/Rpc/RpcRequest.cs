using System.Collections.Generic;

namespace Eth.Rpc
{
    public class RpcRequest : RpcMessage
    {
        public string MethodName { get; set; }
        public IEnumerable<object> Parameters { get; set; }
    }
}
