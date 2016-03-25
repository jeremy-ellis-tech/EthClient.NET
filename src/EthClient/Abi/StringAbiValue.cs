using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eth.Abi
{
    public class StringAbiValue : IAbiValue
    {
        private readonly string _value;

        public StringAbiValue(string value)
        {
            _value = value;
        }

        public byte[] Head { get { return null; } }

        public string Name { get { return "string"; } }

        private byte[] _tail;
        public byte[] Tail
        {
            get
            {
                if (_tail == null)
                {
                    byte[] data = Encoding.UTF8.GetBytes(_value);
                    int length = data.Length;
                    int toPad = 32 - length % 32;
                    List<byte> value = new List<byte>();
                    value.AddRange(BitConverter.IsLittleEndian ? BitConverter.GetBytes(length).Reverse().ToArray() : BitConverter.GetBytes(length));
                    value.AddRange(data);
                    value.AddRange(Enumerable.Repeat<byte>(0, toPad));
                    _tail = value.ToArray();
                }

                return _tail;
            }
        }
    }
}
