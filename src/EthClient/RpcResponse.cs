namespace Eth
{
    public class RpcResponse<T> : RpcMessage
    {
        public T Result { get; set; }
    }
}
