using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Eth.Abi
{
    public class UInt256ArrayAbiValue : IAbiValue
    {
        public UInt256ArrayAbiValue(BigInteger[] value)
        {
            _value = value;
        }

        public UInt256ArrayAbiValue()
        {

        }

        public byte[] Head { get; set; }

        public bool IsDynamic { get { return true; } }

        public string Name
        {
            get
            {
                return "uint256[]";
            }
        }

        private byte[] _tail;
        public byte[] Tail
        {
            get
            {
                if(_tail == null)
                {
                    List<byte> t = new List<byte>();
                    byte[] length = BitConverter.IsLittleEndian ? BitConverter.GetBytes(_value.Length).Reverse().ToArray() : BitConverter.GetBytes(_value.Length);
                    byte[] paddedLength = Enumerable.Repeat<byte>(0x0, 32 - length.Length).Concat(length).ToArray();
                    t.AddRange(paddedLength);
                    foreach (var value in _value)
                    {
                        byte[] v = value.ToByteArray().Reverse().ToArray();
                        byte[] paddedV = Enumerable.Repeat<byte>(0x0, 32 - v.Length).Concat(v).ToArray();
                        t.AddRange(paddedV);
                    }

                    _tail = t.ToArray();
                }

                return _tail;
            }
            set
            {
                _tail = value;
            }
        }

        private BigInteger[] _value;
        public BigInteger[] Value
        {
            get
            {
                if (_value == null)
                {

                }

                return _value;
            }
        }
    }
}
