using Eth.Rpc;
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
            
            switch (reader.TokenType)
            {
                case JsonToken.StartObject:
                    int startingDepth = reader.Depth;
                    while (!(reader.TokenType == JsonToken.EndObject && startingDepth == reader.Depth))
                    {
                        reader.Read();

                        if (reader.TokenType == JsonToken.PropertyName)
                        {
                            string propertyName = reader.Value.ToString();

                            if (String.Equals(propertyName, "type"))
                            {
                                reader.Read();
                                ethLog.Type = serializer.Deserialize<EthLogType?>(reader);
                            }
                            else if (String.Equals(propertyName, "logIndex"))
                            {
                                reader.Read();
                                ethLog.LogIndex = serializer.Deserialize<BigInteger>(reader);
                            }
                            else if (String.Equals(propertyName, "blockNumber"))
                            {
                                reader.Read();
                                ethLog.BlockNumber = serializer.Deserialize<BigInteger>(reader);
                            }
                            else if (String.Equals(propertyName, "blockHash"))
                            {
                                reader.Read();
                                ethLog.BlockHash = serializer.Deserialize<byte[]>(reader);
                            }
                            else if (String.Equals(propertyName, "transactionHash"))
                            {
                                reader.Read();
                                ethLog.TransactionHash = serializer.Deserialize<byte[]>(reader);
                            }
                            else if (String.Equals(propertyName, "transactionIndex"))
                            {
                                reader.Read();
                                ethLog.TransactionIndex = serializer.Deserialize<BigInteger>(reader);
                            }
                            else if (String.Equals(propertyName, "address"))
                            {
                                reader.Read();
                                ethLog.Address = serializer.Deserialize<byte[]>(reader);
                            }
                            else if (String.Equals(propertyName, "data"))
                            {
                                reader.Read();
                                ethLog.Data = serializer.Deserialize<byte[]>(reader);
                            }
                            else if (String.Equals(propertyName, "topics"))
                            {
                                reader.Read();
                                ethLog.Topics = serializer.Deserialize<IEnumerable<byte[]>>(reader);
                            }
                        }
                    }
                    break;
                case JsonToken.StartArray:
                    ethLog.Hashes = serializer.Deserialize<IEnumerable<byte[]>>(reader);
                    break;
                default:
                    throw new NotImplementedException();
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
