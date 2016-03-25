using System;
using System.Collections.Generic;
using System.Linq;

namespace Eth.Abi
{
    public class UInt32AbiValue : IAbiValue
    {
        private readonly uint _value;

        public UInt32AbiValue(uint value)
        {
            _value = value;
        }

        public string Name
        {
            get
            {
                return "uint32";
            }
        }

        private byte[] _head;
        public byte[] Head
        {
            get
            {
                if (_head == null)
                {
                    //BigInteger.ToByteArray() is little-endian order.
                    byte[] value = BitConverter.IsLittleEndian ? BitConverter.GetBytes(_value).Reverse().ToArray() : BitConverter.GetBytes(_value);

                    //Pad to 32 bytes
                    int toPad = 32 - value.Length;
                    List<byte> b = new List<byte>();
                    b.AddRange(Enumerable.Repeat<byte>(0x0, toPad));
                    b.AddRange(value);

                    _head = b.ToArray();
                }

                return _head;
            }
        }

        public byte[] Tail { get { return null; } }
    }
}
