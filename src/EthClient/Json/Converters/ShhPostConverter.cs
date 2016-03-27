using Eth.Rpc;
using Newtonsoft.Json;
using System;

namespace Eth.Json.Converters
{
    public class ShhPostConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(ShhPost);
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

            var obj = value as ShhPost;

            writer.WriteStartObject();

            writer.WritePropertyName("from");
            serializer.Serialize(writer, obj.From);

            writer.WritePropertyName("to");
            serializer.Serialize(writer, obj.To);

            writer.WritePropertyName("topics");
            serializer.Serialize(writer, obj.Topics);

            writer.WritePropertyName("payload");
            serializer.Serialize(writer, obj.Payload);

            writer.WritePropertyName("priority");
            serializer.Serialize(writer, obj.Priority);

            writer.WritePropertyName("ttl");
            serializer.Serialize(writer, obj.Ttl);

            writer.WriteEndObject();
        }

        public override bool CanRead { get { return false; } }
    }
}
