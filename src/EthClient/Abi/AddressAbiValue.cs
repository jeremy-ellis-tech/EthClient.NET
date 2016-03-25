using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eth.Abi
{
    public class AddressAbiValue : IAbiValue
    {
        private readonly byte[] _value;

        public AddressAbiValue(byte[] value)
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
                    int toPad = 32 - _value.Length;
                    List<byte> value = new List<byte>();
                    value.AddRange(Enumerable.Repeat<byte>(0, toPad));
                    value.AddRange(_value);
                    _head = value.ToArray();
                }

                return _head;
            }
        }

        public string Name { get { return "address"; } }

        public byte[] Tail { get { return null; } }
    }
}
