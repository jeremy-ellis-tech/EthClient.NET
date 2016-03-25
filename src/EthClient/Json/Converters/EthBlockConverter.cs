using Eth.Rpc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace Eth.Json.Converters
{
    public class EthBlockConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(EthBlock);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            EthBlock ethBlock = new EthBlock();
            int startingDepth = reader.Depth;

            while (!(reader.TokenType == JsonToken.EndObject && reader.Depth == startingDepth))
            {
                reader.Read();
                if(reader.TokenType == JsonToken.PropertyName)
                {
                    string propertyName = reader.Value.ToString();

                    if(String.Equals(propertyName, "number"))
                    {
                        reader.Read();
                        ethBlock.Number = serializer.Deserialize<BigInteger>(reader);
                    }
                    else if(String.Equals(propertyName, "hash"))
                    {
                        reader.Read();
                        ethBlock.Hash = serializer.Deserialize<byte[]>(reader);
                    }
                    else if (String.Equals(propertyName, "parentHash"))
                    {
                        reader.Read();
                        ethBlock.ParentHash = serializer.Deserialize<byte[]>(reader);
                    }
                    else if (String.Equals(propertyName, "nonce"))
                    {
                        reader.Read();
                        ethBlock.Nonce = serializer.Deserialize<byte[]>(reader);
                    }
                    else if (String.Equals(propertyName, "sha3Uncles"))
                    {
                        reader.Read();
                        ethBlock.Sha3Uncles = serializer.Deserialize<byte[]>(reader);
                    }
                    else if (String.Equals(propertyName, "logsBloom"))
                    {
                        reader.Read();
                        ethBlock.LogsBloom = serializer.Deserialize<byte[]>(reader);
                    }
                    else if (String.Equals(propertyName, "transactionsRoot"))
                    {
                        reader.Read();
                        ethBlock.TransactionRoot = serializer.Deserialize<byte[]>(reader);
                    }
                    else if (String.Equals(propertyName, "stateRoot"))
                    {
                        reader.Read();
                        ethBlock.StateRoot = serializer.Deserialize<byte[]>(reader);
                    }
                    else if (String.Equals(propertyName, "receiptRoot"))
                    {
                        reader.Read();
                        ethBlock.ReceiptRoot = serializer.Deserialize<byte[]>(reader);
                    }
                    else if (String.Equals(propertyName, "miner"))
                    {
                        reader.Read();
                        ethBlock.Miner = serializer.Deserialize<byte[]>(reader);
                    }
                    else if (String.Equals(propertyName, "difficulty"))
                    {
                        reader.Read();
                        ethBlock.Difficulty = serializer.Deserialize<BigInteger>(reader);
                    }
                    else if (String.Equals(propertyName, "totalDifficulty"))
                    {
                        reader.Read();
                        ethBlock.TotalDifficulty = serializer.Deserialize<BigInteger>(reader);
                    }
                    else if (String.Equals(propertyName, "extraData"))
                    {
                        reader.Read();
                        ethBlock.ExtraData = serializer.Deserialize<byte[]>(reader);
                    }
                    else if (String.Equals(propertyName, "size"))
                    {
                        reader.Read();
                        ethBlock.Size = serializer.Deserialize<BigInteger>(reader);
                    }
                    else if (String.Equals(propertyName, "gasLimit"))
                    {
                        reader.Read();
                        ethBlock.GasLimit = serializer.Deserialize<BigInteger>(reader);
                    }
                    else if (String.Equals(propertyName, "gasUsed"))
                    {
                        reader.Read();
                        ethBlock.GasUsed = serializer.Deserialize<BigInteger>(reader);
                    }
                    else if (String.Equals(propertyName, "timestamp"))
                    {
                        reader.Read();
                        ethBlock.TimeStamp = serializer.Deserialize<DateTimeOffset>(reader);
                    }
                    else if (String.Equals(propertyName, "transactions"))
                    {
                        reader.Read();
                        ethBlock.Transactions = serializer.Deserialize<IEnumerable<byte[]>>(reader);
                    }
                    else if (String.Equals(propertyName, "uncles"))
                    {
                        reader.Read();
                        ethBlock.Uncles = serializer.Deserialize<IEnumerable<byte[]>>(reader);
                    }
                }
            }

            return ethBlock;
        }

        public override void WriteJson(JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override bool CanWrite { get { return false; } }
    }
}
