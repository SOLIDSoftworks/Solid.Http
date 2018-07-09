using Newtonsoft.Json;
using Solid.Http.Json.Abstraction;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Solid.Http.Json
{
    internal class JsonResponseDeserializerFactory : IResponseDeserializerFactory
    {
        public JsonResponseDeserializerFactory(IJsonSerializerSettingsProvider provider)
        {
            GetSettings = () => provider.GetJsonSerializerSettings();
        }
        internal JsonResponseDeserializerFactory(JsonSerializerSettings settings)
        {
            GetSettings = () => settings;
        }

        private Func<JsonSerializerSettings> GetSettings { get; }

        public Func<HttpContent, Task<T>> CreateDeserializer<T>()
        {
            return async (content) =>
            {
                var json = await content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(json, GetSettings());
            };            
        }
    }
}
