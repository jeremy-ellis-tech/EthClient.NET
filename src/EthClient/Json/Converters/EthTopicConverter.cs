using Eth.Rpc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Eth.Json.Converters
{
    public class EthTopicConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(EthTopic);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            switch (reader.TokenType)
            {
                case JsonToken.StartArray:
                    IEnumerable<EthTopic> topics = serializer.Deserialize<IEnumerable<EthTopic>>(reader);
                    return new EthTopic(topics.ToArray());
                case JsonToken.String:
                    byte[] value = serializer.Deserialize<byte[]>(reader);
                    return new EthTopic(value);
                case JsonToken.Null:
                    return new EthTopic();
                default:
                    throw new NotImplementedException();
            }
        }

        public override void WriteJson(JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }

            var v = value as EthTopic;

            if (v.Value != null)
            {
                serializer.Serialize(writer, v.Value);
            }
            else
            {
                serializer.Serialize(writer, v.Topics);
            }
        }
    }
}
