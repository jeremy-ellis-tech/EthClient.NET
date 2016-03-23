using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Eth.Json
{
    public class DateTimeOffsetConverter : JsonConverter
    {
        private static readonly DateTimeOffset UnixEpoch = new DateTimeOffset(1970, 1, 1, 0, 0, 0, 0, TimeSpan.Zero);

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(DateTimeOffset);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            switch (reader.TokenType)
            {
                case JsonToken.String:
                    BigInteger bi = serializer.Deserialize<BigInteger>(reader);
                    return UnixEpoch.AddSeconds((int)bi);
                default:
                    throw new NotImplementedException();
            }
        }

        public override void WriteJson(JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
        {
            DateTimeOffset dto = (DateTimeOffset)value;

            double unixTime = dto.Subtract(UnixEpoch).TotalSeconds;

            writer.WriteValue((int)Math.Floor(unixTime));
        }

        public override bool CanRead { get { return true; } }

        public override bool CanWrite { get { return true; } }
    }
}
