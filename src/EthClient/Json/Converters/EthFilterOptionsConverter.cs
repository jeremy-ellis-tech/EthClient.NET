using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eth.Json.Converters
{
    public class EthFilterOptionsConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(EthFilterOptions);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override void WriteJson(JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
        {
            if(value == null)
            {
                writer.WriteNull();
                return;
            }

            var obj = value as EthFilterOptions;

            if(obj == null)
            {
                throw new ArgumentOutOfRangeException("value");
            }

            writer.WriteStartObject();

            writer.WritePropertyName("fromBlock");
            serializer.Serialize(writer, obj.FromBlock);

            writer.WritePropertyName("toBlock");
            serializer.Serialize(writer, obj.ToBlock);

            writer.WritePropertyName("address");
            serializer.Serialize(writer, obj.Address);

            writer.WritePropertyName("topics");
            serializer.Serialize(writer, obj.Topics);

            writer.WriteEndObject();
        }

        public override bool CanRead { get { return false; } }
    }
}
