using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Eth.Json
{
    public class EthTransactionConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(EthTransaction);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            EthTransaction ethTransaction = new EthTransaction();
            int startingDepth = reader.Depth;

            while (!(reader.TokenType == JsonToken.EndObject && startingDepth == reader.Depth))
            {
                reader.Read();

                if (reader.TokenType == JsonToken.PropertyName)
                {
                    string propertyName = reader.Value.ToString();

                    if (String.Equals(propertyName, "hash"))
                    {
                        reader.Read();
                        serializer.Deserialize<byte[]>(reader);
                    }
                    else if (String.Equals(propertyName, "nonce"))
                    {
                        reader.Read();
                        serializer.Deserialize<BigInteger>(reader);
                    }
                    else if (String.Equals(propertyName, "blockHash"))
                    {
                        reader.Read();
                        serializer.Deserialize<byte[]>(reader);
                    }
                    else if (String.Equals(propertyName, "blockNumber"))
                    {
                        reader.Read();
                        serializer.Deserialize<BigInteger>(reader);
                    }
                    else if (String.Equals(propertyName, "transactionIndex"))
                    {
                        reader.Read();
                        serializer.Deserialize<BigInteger>(reader);
                    }
                    else if (String.Equals(propertyName, "from"))
                    {
                        reader.Read();
                        serializer.Deserialize<byte[]>(reader);
                    }
                    else if (String.Equals(propertyName, "to"))
                    {
                        reader.Read();
                        serializer.Deserialize<byte[]>(reader);
                    }
                    else if (String.Equals(propertyName, "value"))
                    {
                        reader.Read();
                        serializer.Deserialize<BigInteger>(reader);
                    }
                    else if (String.Equals(propertyName, "gasPrice"))
                    {
                        reader.Read();
                        serializer.Deserialize<BigInteger>(reader);
                    }
                    else if (String.Equals(propertyName, "gas"))
                    {
                        reader.Read();
                        serializer.Deserialize<BigInteger>(reader);
                    }
                    else if (String.Equals(propertyName, "input"))
                    {
                        reader.Read();
                        serializer.Deserialize<byte[]>(reader);
                    }
                }
            }

            return ethTransaction;
        }

        public override void WriteJson(JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override bool CanWrite { get { return false; } }
    }
}
