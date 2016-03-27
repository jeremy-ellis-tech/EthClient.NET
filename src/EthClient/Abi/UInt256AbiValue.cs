using System.Linq;
using System.Numerics;

namespace Eth.Abi
{
    public class UInt256AbiValue : IAbiValue
    {
        public UInt256AbiValue(BigInteger value)
        {
            _value = value;
        }

        public UInt256AbiValue()
        {

        }

        private byte[] _head;
        public byte[] Head
        {
            get
            {
                if(_head == null)
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
                return "uint256";
            }
        }

        private byte[] _tail;
        public byte[] Tail
        {
            get
            {
                if (_tail == null)
                {
                    _tail = Enumerable.Empty<byte>().ToArray();
                }

                return _tail;
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
                if(_value == null)
                {
                    _value = new BigInteger(_head.Reverse().ToArray());
                }

                return _value.Value;
            }
        }
    }
}
