using System.Linq;
using System.Numerics;

namespace Eth.Abi
{
    public class UInt32AbiValue : IAbiValue
    {
        public UInt32AbiValue(BigInteger value)
        {
            _value = value;
        }

        public UInt32AbiValue()
        {

        }

        private byte[] _head;
        public byte[] Head
        {
            get
            {
                if (_head == null)
                {
                    byte[] value = _value.Value.ToByteArray().Reverse().ToArray();
                    int toPad = 32 - value.Length;
                    _head = Enumerable.Repeat<byte>(0x00, toPad).Concat(value).ToArray();
                }

                return _head;
            }
            set
            {
                _head = value;
            }
        }

        public bool IsDynamic
        {
            get
            {
                return false;
            }
        }

        public string Name
        {
            get
            {
                return "uint32";
            }
        }

        public byte[] Tail
        {
            get
            {
                return null;
            }

            set
            {
                return;
            }
        }

        private BigInteger? _value;
        public BigInteger Value
        {
            get
            {
                if (_value == null)
                {
                    _value = new BigInteger(_head.Reverse().ToArray());
                }

                return _value.Value;
            }
        }
    }
}
