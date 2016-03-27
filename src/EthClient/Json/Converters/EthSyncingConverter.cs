using Eth.Rpc;
using Newtonsoft.Json;
using System;
using System.Numerics;

namespace Eth.Json.Converters
{
    public class EthSyncingConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(EthSyncing);
        }

        private static readonly string StartingBlockKey = "startingBlock";
        private static readonly string CurrentBlockKey = "currentBlock";
        private static readonly string HighestBlockKey = "highestBlock";

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if(reader.TokenType == JsonToken.Boolean)
            {
                return new EthSyncing(false);
            }
            else
            {
                int depth = reader.Depth;

                BigInteger startingBlock;
                BigInteger currentBlock;
                BigInteger highestBlock;

                while (!(reader.TokenType == JsonToken.EndObject && reader.Depth == depth))
                {
                    reader.Read();

                    if(reader.TokenType == JsonToken.PropertyName)
                    {
                        string propertyName = reader.Value.ToString();

                        if(String.Equals(propertyName, StartingBlockKey))
                        {
                            reader.Read();
                            startingBlock = serializer.Deserialize<BigInteger>(reader);
                        }
                        else if (String.Equals(propertyName, CurrentBlockKey))
                        {
                            reader.Read();
                            currentBlock = serializer.Deserialize<BigInteger>(reader);
                        }
                        else if (String.Equals(propertyName, HighestBlockKey))
                        {
                            reader.Read();
                            highestBlock = serializer.Deserialize<BigInteger>(reader);
                        }
                    }
                }

                return new EthSyncing(startingBlock, currentBlock, highestBlock);
            }
        }

        public override void WriteJson(JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
        {
            if(value == null)
            {
                writer.WriteNull();
                return;
            }

            var obj = value as EthSyncing;

            if(obj.IsSynching)
            {
                writer.WriteStartObject();

                writer.WritePropertyName(StartingBlockKey);
                serializer.Serialize(writer, obj.StartingBlock.Value);

                writer.WritePropertyName(CurrentBlockKey);
                serializer.Serialize(writer, obj.CurrentBlock.Value);

                writer.WritePropertyName(HighestBlockKey);
                serializer.Serialize(writer, obj.HighestBlock.Value);

                writer.WriteEndObject();
            }
            else
            {
                writer.WriteValue(false);
            }
        }

        public override bool CanWrite { get { return false; } }
    }
}
