using Eth.Json.Converters;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Numerics;

namespace Eth.Json
{
    public class JsonSerializer : IJsonSerializer
    {
        private readonly JsonSerializerSettings _serializerSettings;

        public JsonSerializer()
        {
            _serializerSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                Converters = new List<JsonConverter>
                {
                    new ByteArrayConverter(),
                    new BigIntegerConverter(),
                    new TimeSpanConverter(),
                    new NullableConverter<BigInteger>(),
                    new DefaultBlockConverter(),
                    new EthSyncingConverter(),
                    new EthBlockConverter(),
                    new DateTimeOffsetConverter(),
                    new EthWorkConverter(),
                    new RpcRequestConverter(),
                    new EthTransactionConverter(),
                    new EthSolidityContractConverter(),
                    new ContractInfoConverter(),
                    new AbiDefinitionConverter(),
                    new FunctionInputOutputConverter(),
                    new EthCallConverter(),
                    new ShhPostConverter(),
                }
            };
        }

        public T Deserialize<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json, _serializerSettings);
        }

        public string Serialize(object obj)
        {
            return JsonConvert.SerializeObject(obj, _serializerSettings);
        }
    }
}
