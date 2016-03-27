using Eth.Rpc;
using Newtonsoft.Json;
using System;

namespace Eth.Json.Converters
{
    public class EthTransactionConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(EthTransaction);
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

            EthTransaction transaction = value as EthTransaction;

            writer.WriteStartObject();

            writer.WritePropertyName("from");
            serializer.Serialize(writer, transaction.From);

            writer.WritePropertyName("to");
            serializer.Serialize(writer, transaction.To);

            writer.WritePropertyName("gas");
            serializer.Serialize(writer, transaction.Gas);

            writer.WritePropertyName("gasPrice");
            serializer.Serialize(writer, transaction.GasPrice);

            writer.WritePropertyName("value");
            serializer.Serialize(writer, transaction.Value);

            writer.WritePropertyName("data");
            serializer.Serialize(writer, transaction.Data);

            writer.WritePropertyName("nonce");
            serializer.Serialize(writer, transaction.Nonce);

            writer.WriteEndObject();
        }

        public override bool CanRead { get { return false; } }
    }
}
