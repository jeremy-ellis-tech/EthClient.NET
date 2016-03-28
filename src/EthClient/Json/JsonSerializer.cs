using Eth.Json.Converters;
using Eth.Rpc;
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

                    new EthLogConverter(),
                    new DefaultBlockConverter(),
                    new EthSyncingConverter(),
                    new EthBlockConverter(),
                    new EthLogTypeConverter(),
                    new NullableConverter<EthLogType>(),
                    new DateTimeOffsetConverter(),
                    new EthTopicConverter(),
                    new EthWorkConverter(),
                    new EthFilterOptionsConverter(),
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
