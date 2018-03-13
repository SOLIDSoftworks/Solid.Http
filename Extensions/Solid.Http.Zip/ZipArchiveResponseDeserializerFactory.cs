using Solid.Http.Abstractions;
using Solid.Http.Zip.Abstraction;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Solid.Http.Zip
{
    internal class ZipArchiveResponseDeserializerFactory : IResponseDeserializerFactory
    {
        public ZipArchiveResponseDeserializerFactory(IZipSerializerSettingsProvider provider)
        {
            GetSettings = () => provider.GetZipSerializerSettings();
        }

        internal ZipArchiveResponseDeserializerFactory(ZipArchiveMode settings)
        {
            GetSettings = () => settings;
        }

        private Func<ZipArchiveMode> GetSettings { get; }
        public Func<HttpContent, Task<T>> CreateDeserializer<T>()
        {
            return async (content) =>
            {
                var ms = new MemoryStream();
                await content.CopyToAsync(ms);
                var arch = new ZipArchive(ms, GetSettings());
                return (T)((object)arch);
            };
        }
    }
}
