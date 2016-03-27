using Eth.Rpc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Eth.Json.Converters
{
    public class AbiDefinitionConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(AbiDefinition);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            AbiDefinition abiDefinition = new AbiDefinition();
            int startingDepth = reader.Depth;

            while (!(reader.TokenType == JsonToken.EndObject && reader.Depth == startingDepth))
            {
                reader.Read();
                if (reader.TokenType == JsonToken.PropertyName)
                {
                    string propertyName = reader.Value.ToString();

                    if (String.Equals(propertyName, "contant"))
                    {
                        reader.Read();
                        abiDefinition.Constant = serializer.Deserialize<bool>(reader);
                    }
                    else if (String.Equals(propertyName, "name"))
                    {
                        reader.Read();
                        abiDefinition.Name = reader.Value.ToString();
                    }
                    else if (String.Equals(propertyName, "type"))
                    {
                        reader.Read();
                        abiDefinition.Type = reader.Value.ToString();
                    }
                    else if (String.Equals(propertyName, "inputs"))
                    {
                        reader.Read();
                        abiDefinition.Inputs = serializer.Deserialize<List<FunctionInputOutput>>(reader);
                    }
                    else if (String.Equals(propertyName, "outputs"))
                    {
                        reader.Read();
                        abiDefinition.Outputs = serializer.Deserialize<List<FunctionInputOutput>>(reader);
                    }
                }
            }

            return abiDefinition;
        }

        public override void WriteJson(JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override bool CanWrite { get { return false; } }
    }
}
