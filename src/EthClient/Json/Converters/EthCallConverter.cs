using Eth.Rpc;
using Newtonsoft.Json;
using System;

namespace Eth.Json.Converters
{
    public class EthCallConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(EthCall);
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

            EthCall call = value as EthCall;

            writer.WriteStartObject();

            writer.WritePropertyName("from");
            serializer.Serialize(writer, call.From);

            writer.WritePropertyName("to");
            serializer.Serialize(writer, call.To);

            writer.WritePropertyName("gas");
            serializer.Serialize(writer, call.Gas);

            writer.WritePropertyName("gasPrice");
            serializer.Serialize(writer, call.GasPrice);

            writer.WritePropertyName("value");
            serializer.Serialize(writer, call.Value);

            writer.WritePropertyName("data");
            serializer.Serialize(writer, call.Data);

            writer.WriteEndObject();
        }

        public override bool CanRead { get { return false; } }
    }
}
