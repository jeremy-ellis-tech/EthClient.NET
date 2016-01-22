using System;

namespace Eth
{
    public class EthException : Exception
    {
        public int ErrorCode { get; private set; }

        internal EthException(int errorCode, string message) : base(message)
        {
            ErrorCode = errorCode;
        }
    }
}
