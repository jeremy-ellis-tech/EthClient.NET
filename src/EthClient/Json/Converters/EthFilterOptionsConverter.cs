using Eth.Rpc;
using Newtonsoft.Json;
using System;

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
            if(obj.Topics == null)
            {
                writer.WriteNull();
            }
            else
            {
                bool wrapInArray = obj.Topics.Topics == null;
                if (wrapInArray) writer.WriteStartArray();
                serializer.Serialize(writer, obj.Topics);
                if (wrapInArray) writer.WriteEndArray();
            }

            writer.WriteEndObject();
        }

        public override bool CanRead { get { return false; } }
    }
}
