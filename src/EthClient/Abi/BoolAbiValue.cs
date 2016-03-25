using System.Collections.Generic;
using System.Linq;

namespace Eth.Abi
{
    public class BoolAbiValue : IAbiValue
    {
        private readonly bool _value;

        public BoolAbiValue(bool value)
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
                    byte value = _value ? (byte)0x01 : (byte)0x00;
                    List<byte> h = new List<byte>();
                    h.AddRange(Enumerable.Repeat<byte>(0x00, 31));
                    h.Add(value);
                    _head = h.ToArray();
                }

                return _head;
            }
        }

        public string Name { get { return "bool"; } }

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
