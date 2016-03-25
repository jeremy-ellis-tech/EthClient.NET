using System.Collections.Generic;
using System.Linq;

namespace Eth.Abi
{
    public class UInt8AbiValue : IAbiValue
    {
        private readonly byte _value;

        public UInt8AbiValue(byte value)
        {
            _value = value;
        }

        private byte[] _head;
        public byte[] Head
        {
            get
            {
                if (_head == null)
                {
                    List<byte> value = new List<byte>();
                    value.AddRange(Enumerable.Repeat<byte>(0x00, 31));
                    value.Add(_value);
                    _head = value.ToArray();
                }

                return _head;
            }
        }

        public string Name { get { return "uint8"; } }

        public byte[] Tail { get { return null; } }
    }
}
