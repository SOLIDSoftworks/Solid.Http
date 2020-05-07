using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Solid.Http.Json
{
    internal class SystemTextJsonDeserializer : IDeserializer, IDisposable
    {
        private SolidHttpJsonOptions _options;
        private IDisposable _optionsChangeToken;

        public SystemTextJsonDeserializer(IOptionsMonitor<SolidHttpJsonOptions> monitor)
        {
            _options = monitor.CurrentValue;
            _optionsChangeToken = monitor.OnChange((options, _) => _options = options);
        }
        public bool CanDeserialize(string mediaType) => _options.SupportedMediaTypes.Any(m => m.MediaType.Equals(mediaType, StringComparison.OrdinalIgnoreCase));

        public async ValueTask<T> DeserializeAsync<T>(HttpContent content)
        {
            using (var stream = await content.ReadAsStreamAsync())
            {
                return await JsonSerializer.DeserializeAsync<T>(stream, _options.SerializerOptions);
            }
        }

        public void Dispose()
        {
            _optionsChangeToken?.Dispose();
        }
    }
}
