using System.Collections.Generic;
using System.Linq;

namespace Eth.Rpc
{
    public class EthTopic
    {
        public EthTopic(byte[] value)
        {
            Value = value;
        }

        public EthTopic(params EthTopic[] topics)
        {
            Topics = topics;
        }

        public EthTopic()
        {

        }

        public IEnumerable<EthTopic> Topics { get; private set; }

        public byte[] Value { get; private set; }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj)) return true;

            var other = obj as EthTopic;

            if (other == null) return false;

            if(Value != null && other.Value != null)
            {
                return Equals(Value, other.Value);
            }
            else if(Topics == null && other.Topics == null && Value == null && other.Value == null)
            {
                return true;
            }
            else
            {
                return Equals(Topics, other.Topics);
            }
        }

        public override int GetHashCode()
        {
            if (Value != null)
            {
                return Value.GetHashCode();
            }
            else if (Topics == null && Value == null)
            {
                return 0;
            }
            else
            {
                return Topics.GetHashCode();
            }
        }
    }
}
