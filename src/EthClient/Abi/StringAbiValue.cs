using System.Text;

namespace Eth.Abi
{
    public class StringAbiValue : IAbiValue
    {
        //In ABI, string acts as UTF8 encoded bytes type.
        //So just UTF8 encode and let the AbiBytesValue handle logic.
        private BytesAbiValue _value;
        public StringAbiValue(string value)
        {
            _value = new BytesAbiValue(Encoding.UTF8.GetBytes(value));
        }

        public StringAbiValue()
        {

        }

        public byte[] Head
        {
            get
            {
                return _value.Head;
            }
            set
            {
                _value.Head = value;
            }
        }

        public bool IsDynamic { get { return true; } }

        public string Name { get { return "string"; } }

        public byte[] Tail
        {
            get
            {
                return _value.Tail;
            }

            set
            {
                _value.Tail = value;
            }
        }

        public string Value { get { return Encoding.UTF8.GetString(_value.Value, 0, _value.Value.Length); } }

        public static explicit operator string(StringAbiValue value)
        {
            return value.Value;
        }
    }
}
