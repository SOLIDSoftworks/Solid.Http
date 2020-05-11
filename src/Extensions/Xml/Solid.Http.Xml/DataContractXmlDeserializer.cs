using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Solid.Http.Xml
{
    internal class DataContractXmlDeserializer : IDeserializer, IDisposable
    {
        private SolidHttpXmlOptions _options;
        private IDisposable _optionsChangeToken;

        public DataContractXmlDeserializer(IOptionsMonitor<SolidHttpXmlOptions> monitor)
        {
            _options = monitor.CurrentValue;
            _optionsChangeToken = monitor.OnChange((options, _) => _options = options);
        }

        public bool CanDeserialize(string mediaType, Type _) => _options.SupportedMediaTypes.Any(m => m.MediaType.Equals(mediaType, StringComparison.OrdinalIgnoreCase));

        public ValueTask<T> DeserializeAsync<T>(HttpContent content)
            => DeserializeAsync<T>(content, _options.Settings);

        public async ValueTask<T> DeserializeAsync<T>(HttpContent content, DataContractSerializerSettings settings)
        {
            using (var stream = await content.ReadAsStreamAsync())
            {
                var serializer = new DataContractSerializer(typeof(T), settings);
                return (T)serializer.ReadObject(stream);
            }
        }

        public void Dispose()
        {
            _optionsChangeToken?.Dispose();
        }
    }
}
