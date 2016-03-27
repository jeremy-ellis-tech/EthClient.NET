using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eth.Abi
{
    public class BytesAbiValue : IAbiValue
    {
        public BytesAbiValue(byte[] value)
        {
            _value = value;
        }

        public BytesAbiValue()
        {

        }

        public byte[] Head { get; set; }

        public bool IsDynamic { get { return true; } }

        public string Name { get { return "bytes"; } }

        private byte[] _tail;
        public byte[] Tail
        {
            get
            {
                if(_tail == null)
                {
                    byte[] length = BitConverter.IsLittleEndian ? BitConverter.GetBytes(_value.Length).Reverse().ToArray() : BitConverter.GetBytes(_value.Length).ToArray();
                    byte[] paddedLength = Enumerable.Repeat<byte>(0x00, 32 - length.Length).Concat(length).ToArray();

                    _tail = paddedLength.Concat(_value).Concat(Enumerable.Repeat<byte>(0x00, 32 - _value.Length % 32)).ToArray();
                }

                return _tail;
            }

            set
            {
                _tail = value;
            }
        }

        private byte[] _value;
        public byte[] Value
        {
            get
            {
                if(_value == null)
                {

                }

                return _value;
            }
        }

        public static explicit operator byte[](BytesAbiValue value)
        {
            return value.Value;
        }
    }
}
