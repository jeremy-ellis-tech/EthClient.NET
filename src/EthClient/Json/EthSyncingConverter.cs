using Newtonsoft.Json;
using System;

namespace Eth.Json
{
    public class EthSyncingConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Eth.EthSyncing);
        }

        private static readonly string StartingBlockKey = "startingBlock";
        private static readonly string CurrentBlockKey = "currentBlock";
        private static readonly string HighestBlockKey = "highestBlock";

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            switch (reader.TokenType)
            {
                case JsonToken.String:
                    return null;
                case JsonToken.Boolean:
                    return new Eth.EthSyncing();
                case JsonToken.Null:
                    return null;
                default:
                    throw new NotImplementedException();
            }
        }

        public override void WriteJson(JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
        {
            if(value == null)
            {
                writer.WriteNull();
                return;
            }

            var obj = value as Eth.EthSyncing;

            if(obj == null) throw new JsonSerializationException("value is not of type EthSyncing");

            if(obj.IsSynching)
            {
                writer.WritePropertyName(StartingBlockKey);
                serializer.Serialize(writer, obj.StartingBlock.Value);

                writer.WritePropertyName(CurrentBlockKey);
                serializer.Serialize(writer, obj.CurrentBlock.Value);

                writer.WritePropertyName(HighestBlockKey);
                serializer.Serialize(writer, obj.HighestBlock.Value);
            }
            else
            {
                writer.WriteValue(false);
            }
        }

        public override bool CanRead { get { return true; } }

        public override bool CanWrite { get { return true; } }
    }
}
