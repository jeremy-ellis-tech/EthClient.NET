namespace Eth.Rpc
{
    public abstract class RpcMessage
    {
        public int ID { get; set; }
        public string JsonRpc { get; set; }
    }
}
