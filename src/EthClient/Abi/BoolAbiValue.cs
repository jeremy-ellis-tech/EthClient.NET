using System.Collections.Generic;
using System.Linq;

namespace Eth.Abi
{
    public class BoolAbiValue : IAbiValue
    {
        public BoolAbiValue(bool value)
        {
            _value = value;
        }

        public BoolAbiValue()
        {

        }

        private byte[] _head;
        public byte[] Head
        {
            get
            {
                if(_head == null)
                {
                    IList<byte> padding = Enumerable.Repeat<byte>(0x00, 31).ToList();
                    byte value = _value.Value ? (byte)0x01 : (byte)0x00;
                    padding.Add(value);
                    _head = padding.ToArray();
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
                return "bool";
            }
        }

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

            set
            {
                return;
            }
        }

        private bool? _value;
        public bool Value
        {
            get
            {
                if(_value == null)
                {
                    _value = Head.Last() == 0x01;
                }

                return _value.Value;
            }
        }

        public static explicit operator bool(BoolAbiValue value)
        {
            return value.Value;
        }
    }
}
