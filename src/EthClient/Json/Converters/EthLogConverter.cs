using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace Eth.Json.Converters
{
    public class EthLogConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(EthLog);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            EthLog ethLog = new EthLog();
            int startingDepth = reader.Depth;

            while (!(reader.TokenType == JsonToken.EndObject && startingDepth == reader.Depth))
            {
                reader.Read();

                if(reader.TokenType == JsonToken.PropertyName)
                {
                    string propertyName = reader.Value.ToString();

                    if(String.Equals(propertyName, "type"))
                    {
                        reader.Read();
                        serializer.Deserialize<EthLogType>(reader);
                    }
                    else if (String.Equals(propertyName, "logIndex"))
                    {
                        reader.Read();
                        serializer.Deserialize<BigInteger>(reader);
                    }
                    else if(String.Equals(propertyName, "blockNumer"))
                    {
                        reader.Read();
                        serializer.Deserialize<BigInteger>(reader);
                    }
                    else if (String.Equals(propertyName, "blockHash"))
                    {
                        reader.Read();
                        serializer.Deserialize<byte[]>(reader);
                    }
                    else if (String.Equals(propertyName, "transactionHash"))
                    {
                        reader.Read();
                        serializer.Deserialize<byte[]>(reader);
                    }
                    else if (String.Equals(propertyName, "transactionIndex"))
                    {
                        reader.Read();
                        serializer.Deserialize<BigInteger>(reader);
                    }
                    else if (String.Equals(propertyName, "address"))
                    {
                        reader.Read();
                        serializer.Deserialize<byte[]>(reader);
                    }
                    else if (String.Equals(propertyName, "data"))
                    {
                        reader.Read();
                        serializer.Deserialize<byte[]>(reader);
                    }
                    else if (String.Equals(propertyName, "topics"))
                    {
                        reader.Read();
                        serializer.Deserialize<IEnumerable<byte[]>>(reader);
                    }
                }
            }

            return ethLog;
        }

        public override void WriteJson(JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override bool CanWrite { get { return false; } }
    }
}
