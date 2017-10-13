using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FluentHttp
{
    internal class JsonResponseDeserializer : IResponseDeserializer
    {
        private JsonSerializerSettings _settings;

        public JsonResponseDeserializer(JsonSerializerSettings settings)
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
