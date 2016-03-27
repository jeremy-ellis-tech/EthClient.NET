using Eth.Rpc;
using Newtonsoft.Json;
using System;

namespace Eth.Json.Converters
{
    public class FunctionInputOutputConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(FunctionInputOutput);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            FunctionInputOutput functionInputOutput = new FunctionInputOutput();
            int startingDepth = reader.Depth;

            while (!(reader.TokenType == JsonToken.EndObject && reader.Depth == startingDepth))
            {
                reader.Read();
                if (reader.TokenType == JsonToken.PropertyName)
                {
                    string propertyName = reader.Value.ToString();

                    if (String.Equals(propertyName, "name"))
                    {
                        reader.Read();
                        functionInputOutput.Name = reader.Value.ToString();
                    }
                    else if (String.Equals(propertyName, "type"))
                    {
                        reader.Read();
                        functionInputOutput.Type = reader.Value.ToString();
                    }
                }
            }

            return functionInputOutput;
        }

        public override void WriteJson(JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override bool CanWrite { get { return false; } }
    }
}
