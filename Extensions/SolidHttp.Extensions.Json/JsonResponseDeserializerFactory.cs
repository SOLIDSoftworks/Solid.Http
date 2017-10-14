using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SolidHttp
{
    internal class JsonResponseDeserializerFactory : IResponseDeserializerFactory
    {
        private JsonSerializerSettings _settings;

        public JsonResponseDeserializerFactory(JsonSerializerSettings settings)
        {
            _settings = settings;
        }
        public Func<HttpContent, Task<T>> CreateDeserializer<T>()
        {
            return async (content) =>
            {
                var json = await content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(json, _settings);
            };            
        }
    }
}
