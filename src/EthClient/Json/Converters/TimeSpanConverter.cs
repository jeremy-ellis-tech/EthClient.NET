using Newtonsoft.Json;
using System;
using System.Numerics;

namespace Eth.Json.Converters
{
    public class TimeSpanConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(TimeSpan);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            BigInteger value = serializer.Deserialize<BigInteger>(reader);
            return TimeSpan.FromSeconds((double)value);
        }

        public override void WriteJson(JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
        {
            if(value == null)
            {
                writer.WriteNull();
                return;
            }

            TimeSpan obj = (TimeSpan)value;

            writer.WriteValue(obj.TotalSeconds);
        }
    }
}
