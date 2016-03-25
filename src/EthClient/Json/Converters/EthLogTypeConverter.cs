using Eth.Rpc;
using Newtonsoft.Json;
using System;

namespace Eth.Json.Converters
{
    public class EthLogTypeConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(EthLogType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            switch (reader.Value.ToString())
            {
                case "pending":
                    return EthLogType.Pending;
                case "mined":
                    return EthLogType.Mined;
                default:
                    throw new NotImplementedException();
            }
        }

        public override void WriteJson(JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
        {
            if(value == null)
            {
                writer.WriteNull();
                return;
            }

            EthLogType type = (EthLogType)value;
            writer.WriteValue(type.ToString().ToLowerInvariant());
        }
    }
}
