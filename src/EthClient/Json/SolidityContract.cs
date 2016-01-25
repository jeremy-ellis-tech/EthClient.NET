using Newtonsoft.Json;
using System.Collections.Generic;

namespace Eth.Json
{
    public class SolidityContract
    {
        public string ContractName { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("info")]
        public Info Info { get; set; }
    }

    public class Info
    {
        [JsonProperty("source")]
        public string Source { get; set; }

        [JsonProperty("language")]
        public string Language { get; set; }

        [JsonProperty("languageVersion")]
        public string LanguageVersion { get; set; }

        [JsonProperty("compilerVersion")]
        public string CompilerVersion { get; set; }

        [JsonProperty("compilerOptions")]
        public string CompilerOptions { get; set; }

        [JsonProperty("abiDefinition")]
        public List<AbiDefinition> AbiDefinition { get; set; }

        [JsonProperty("userDoc")]
        public UserDoc UserDoc { get; set; }

        [JsonProperty("developerDoc")]
        public DeveloperDoc DeveloperDoc { get; set; }
    }

    public class AbiDefinition
    {
        [JsonProperty("constant")]
        public bool Constant { get; set; }

        [JsonProperty("inputs")]
        public List<object> Inputs { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("outputs")]
        public List<object> Outputs { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }

    public class UserDoc
    {
        [JsonProperty("methods")]
        public object Methods { get; set; }
    }

    public class DeveloperDoc
    {
        [JsonProperty("methods")]
        public object Methods { get; set; }
    }
}
