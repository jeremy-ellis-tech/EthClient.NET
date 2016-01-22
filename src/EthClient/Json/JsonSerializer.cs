using Newtonsoft.Json;

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
                Formatting = Formatting.Indented
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
