using Eth.Utilities;
using Newtonsoft.Json;
using System;

namespace Eth.Json.Converters
{
    public class ByteArrayConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(byte[]);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            switch (reader.TokenType)
            {
                case JsonToken.String:
                    return EthHex.HexStringToByteArray(reader.Value.ToString());
                case JsonToken.Null:
                    return null;
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

            byte[] arr = value as byte[];

            if(arr == null)
            {
                throw new ArgumentOutOfRangeException("value");
            }

            writer.WriteValue(EthHex.ToHexString(arr));
        }
    }
}
