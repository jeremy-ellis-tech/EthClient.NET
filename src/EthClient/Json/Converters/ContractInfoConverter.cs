using Eth.Rpc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Eth.Json.Converters
{
    public class ContractInfoConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(ContractInfo);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            ContractInfo contractInfo = new ContractInfo();
            int startingDepth = reader.Depth;

            while (!(reader.TokenType == JsonToken.EndObject && reader.Depth == startingDepth))
            {
                reader.Read();
                if (reader.TokenType == JsonToken.PropertyName)
                {
                    string propertyName = reader.Value.ToString();

                    if (String.Equals(propertyName, "source"))
                    {
                        reader.Read();
                        contractInfo.Source = reader.Value.ToString();
                    }
                    else if (String.Equals(propertyName, "language"))
                    {
                        reader.Read();
                        contractInfo.Language = reader.Value.ToString();
                    }
                    else if (String.Equals(propertyName, "languageVersion"))
                    {
                        reader.Read();
                        contractInfo.LanguageVersion = reader.Value.ToString();
                    }
                    else if (String.Equals(propertyName, "compilerVersion"))
                    {
                        reader.Read();
                        contractInfo.CompilerVersion = reader.Value.ToString();
                    }
                    else if (String.Equals(propertyName, "compilerOptions"))
                    {
                        reader.Read();
                        contractInfo.CompilerOptions = reader.Value.ToString();
                    }
                    else if (String.Equals(propertyName, "abiDefinition"))
                    {
                        reader.Read();
                        contractInfo.AbiDefinition = serializer.Deserialize<List<AbiDefinition>>(reader);
                    }
                }
            }

            return contractInfo;
        }

        public override void WriteJson(JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override bool CanWrite { get { return false; } }
    }
}
