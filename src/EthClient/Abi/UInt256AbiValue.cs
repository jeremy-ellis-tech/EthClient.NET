using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Eth.Abi
{
    public class UInt256AbiValue : IAbiValue
    {
        private readonly BigInteger _value;

        public UInt256AbiValue(BigInteger value)
        {
            _value = value;
        }

        private byte[] _head;
        public byte[] Head
        {
            get
            {
                if(_head == null)
                {
                    //BigInteger.ToByteArray() is little-endian order.
                    byte[] value = _value.ToByteArray().Reverse().ToArray();

                    //Pad to 32 bytes
                    int toPad = 32 - value.Length % 32;
                    List<byte> b = new List<byte>();
                    b.AddRange(Enumerable.Repeat<byte>(0x0, toPad));
                    b.AddRange(value);

                    _head = b.ToArray();
                }

                return _head;
            }
        }

        public string Name { get { return "uint256"; } }

        private byte[] _tail;
        public byte[] Tail
        {
            get
            {
                if(_tail == null)
                {
                    _tail = Enumerable.Empty<byte>().ToArray();
                }

                return _tail;
            }
        }
    }
}
