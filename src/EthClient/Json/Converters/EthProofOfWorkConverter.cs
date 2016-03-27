using Eth.Rpc;
using Newtonsoft.Json;
using System;

namespace Eth.Json.Converters
{
    public class EthProofOfWorkConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(EthProofOfWork);
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

            EthProofOfWork proofOfWork = value as EthProofOfWork;

            writer.WriteStartArray();
            serializer.Serialize(writer, proofOfWork.Nonce);
            serializer.Serialize(writer, proofOfWork.PowHash);
            serializer.Serialize(writer, proofOfWork.MixDigest);
            writer.WriteEndArray();
        }

        public override bool CanRead { get { return false; } }
    }
}
