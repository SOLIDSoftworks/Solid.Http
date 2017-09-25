using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FluentHttp
{
    internal class FluentHttpJsonSerializer : IFluentHttpSerializer
    {
        private JsonSerializerSettings _settings;

        public FluentHttpJsonSerializer(JsonSerializerSettings settings)
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
