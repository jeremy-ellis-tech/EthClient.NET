namespace Eth.Rpc
{
    public class RpcError : RpcMessage
    {
        public EthError Error { get; set; }
    }
}
