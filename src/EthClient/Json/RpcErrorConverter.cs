using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eth.Json
{
    public class RpcErrorConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(RpcError);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            RpcError rpcError = new RpcError();

            while (reader.Read())
            {
                switch (reader.TokenType)
                {
                    case JsonToken.PropertyName:
                        if(reader.Value.ToString() == "jsonrpc")
                        {
                            reader.Read();
                            rpcError.JsonRpc = reader.Value.ToString();
                        }
                        else if (reader.Value.ToString() == "id")
                        {
                            reader.Read();
                            rpcError.ID = serializer.Deserialize<int>(reader);
                        }
                        else if (reader.Value.ToString() == "error")
                        {

                        }
                        break;
                    default:
                        break;
                }
            }

            return rpcError;
        }

        public override void WriteJson(JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override bool CanWrite { get { return false; } }
    }
}
