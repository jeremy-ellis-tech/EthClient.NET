using Newtonsoft.Json;
using System;

namespace Eth.Json.Converters
{
    public class NullableConverter<T> : JsonConverter where T : struct
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(T?);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            switch (reader.TokenType)
            {
                case JsonToken.Null:
                    return null;
                default:
                    return serializer.Deserialize<T>(reader);
            }
        }

        public override void WriteJson(JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
        {
            if(value == null)
            {
                writer.WriteNull();
                return;
            }

            var obj = value as T?;

            if(obj == null)
            {
                throw new ArgumentOutOfRangeException("value");
            }

            serializer.Serialize(writer, obj);
        }
    }
}
