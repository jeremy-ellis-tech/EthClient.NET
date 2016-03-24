namespace Eth
{
    public class RpcError : RpcMessage
    {
        public EthError Error { get; set; }
    }
}
