using System;

namespace Eth
{
    public class EthException : Exception
    {
        private readonly int _errorCode;

        public int ErrorCode { get { return _errorCode; } }

        internal EthException(int errorCode, string message) : base(message)
        {
            _errorCode = errorCode;
        }
    }
}
