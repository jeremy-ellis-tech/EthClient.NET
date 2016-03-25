using System;

namespace Eth.Rpc
{
    public class EthException : Exception
    {
        public int ErrorCode { get; private set; }

        public EthException(int errorCode, string message) : base(message)
        {
            ErrorCode = errorCode;
        }
    }
}
