using Eth.Utilities;
using Newtonsoft.Json;
using System;
using System.Numerics;

namespace Eth.Json
{
    public class BigIntegerConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(BigInteger);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            string json = reader.Value.ToString();
            return EthHex.HexStringToInt(json);
        }

        public override void WriteJson(JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
        {
            if(value == null)
            {
                throw new ArgumentNullException("value");
            }

            BigInteger bi = (BigInteger)value;

            writer.WriteValue(EthHex.ToHexString(bi));
        }
    }
}
