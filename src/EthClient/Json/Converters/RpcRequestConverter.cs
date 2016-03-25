using Eth.Rpc;
using Newtonsoft.Json;
using System;

namespace Eth.Json.Converters
{
    public class RpcRequestConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(RpcRequest);
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

            var obj = value as RpcRequest;

            if(obj == null)
            {
                throw new ArgumentOutOfRangeException("value");
            }

            writer.WriteStartObject();

            writer.WritePropertyName("id");
            writer.WriteValue(obj.ID);

            writer.WritePropertyName("jsonrpc");
            writer.WriteValue(obj.JsonRpc);

            writer.WritePropertyName("method");
            writer.WriteValue(obj.MethodName);

            writer.WritePropertyName("params");
            serializer.Serialize(writer, obj.Parameters);

            writer.WriteEndObject();
        }

        public override bool CanRead { get { return false; } }
    }
}
