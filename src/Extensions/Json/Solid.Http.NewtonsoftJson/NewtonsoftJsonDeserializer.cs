using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Solid.Http.NewtonsoftJson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Solid.Http.Json
{
    internal class NewtonsoftJsonDeserializer : IDeserializer, IDisposable
    {
        private SolidHttpNewtonsoftJsonOptions _options;
        private IDisposable _optionsChangeToken;

        public NewtonsoftJsonDeserializer(IOptionsMonitor<SolidHttpNewtonsoftJsonOptions> monitor)
        {
            _options = monitor.CurrentValue;
            _optionsChangeToken = monitor.OnChange((options, _) => _options = options);
        }
        public bool CanDeserialize(string mediaType) => _options.SupportedMediaTypes.Any(m => m.MediaType.Equals(mediaType, StringComparison.OrdinalIgnoreCase));

        public async ValueTask<T> DeserializeAsync<T>(HttpContent content)
        {
            var json = await content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(json, _options.SerializerSettings);           
        }

        public void Dispose()
        {
            _optionsChangeToken?.Dispose();
        }
    }
}
