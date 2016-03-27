using System.Collections.Generic;

namespace Eth.Rpc
{
    public class EthSolidityContract
    {
        public string ContractName { get; set; }
        public byte[] Code { get; set; }
        public ContractInfo Info { get; set; }
    }

    public class ContractInfo
    {
        public string Source { get; set; }
        public string Language { get; set; }
        public string LanguageVersion { get; set; }
        public string CompilerVersion { get; set; }
        public string CompilerOptions { get; set; }
        public List<AbiDefinition> AbiDefinition { get; set; }
        public UserDoc UserDoc { get; set; }
        public DeveloperDoc DeveloperDoc { get; set; }
    }

    public class AbiDefinition
    {
        public bool Constant { get; set; }
        public List<FunctionInputOutput> Inputs { get; set; }
        public string Name { get; set; }
        public List<FunctionInputOutput> Outputs { get; set; }
        public string Type { get; set; }
    }

    public class FunctionInputOutput
    {
        public string Name { get; set; }
        public string Type { get; set; }
    }

    public class UserDoc
    {
        public object Methods { get; set; }
    }

    public class DeveloperDoc
    {
        public object Methods { get; set; }
    }
}
