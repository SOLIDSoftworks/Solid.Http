using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Solid.Http.Zip
{
    internal class ZipArchiveDeserializer : IDeserializer, IDisposable
    {
        private SolidHttpZipOptions _options;
        private IDisposable _optionsChangeToken;

        public ZipArchiveDeserializer(IOptionsMonitor<SolidHttpZipOptions> monitor)
        {
            _options = monitor.CurrentValue;
            _optionsChangeToken = monitor.OnChange((options, _) => _options = options);
        }

        public bool CanDeserialize(string mediaType) => _options.SupportedMediaTypes.Any(m => m.MediaType.Equals(mediaType, StringComparison.OrdinalIgnoreCase));

        public async ValueTask<T> DeserializeAsync<T>(HttpContent content)
        {
            if (typeof(T) != typeof(ZipArchive)) throw new ArgumentException($"Type '{typeof(T).FullName}' not supported by {nameof(ZipArchiveDeserializer)}", nameof(T));
            var archive = await DeserializeAsync(content, _options.Mode);
            return (T)(object)archive;

        }
        public async ValueTask<ZipArchive> DeserializeAsync(HttpContent content, ZipArchiveMode mode)
        {
            using (var stream = await content.ReadAsStreamAsync())
            {
                var archive = new ZipArchive(stream, mode);
                return archive;
            }
        }

        public void Dispose()
        {
            _optionsChangeToken?.Dispose();
        }
    }
}
