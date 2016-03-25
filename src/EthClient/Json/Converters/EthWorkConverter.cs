using Eth.Rpc;
using Newtonsoft.Json;
using System;

namespace Eth.Json.Converters
{
    public class EthWorkConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(EthWork);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            EthWork work = new EthWork();
            reader.Read();
            work.BlockHash = serializer.Deserialize<byte[]>(reader);
            reader.Read();
            work.SeedHash = serializer.Deserialize<byte[]>(reader);
            reader.Read();
            work.BoundaryCondition = serializer.Deserialize<byte[]>(reader);
            reader.Read();

            return work;
        }

        public override void WriteJson(JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override bool CanWrite { get { return false; } }
    }
}
