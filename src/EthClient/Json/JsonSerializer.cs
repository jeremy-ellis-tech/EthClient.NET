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
                    new DefaultBlockConverter(),
                    new EthSyncingConverter(),
                    new NullableConverter<BigInteger>(),
                    new EthBlockConverter(),
                    new DateTimeOffsetConverter(),
                    new EthWorkConverter(),
                    new RpcRequestConverter(),
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
