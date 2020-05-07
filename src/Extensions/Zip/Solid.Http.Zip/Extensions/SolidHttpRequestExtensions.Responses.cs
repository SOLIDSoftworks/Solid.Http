using Microsoft.Extensions.Options;
using Solid.Http.Zip;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Solid.Http
{
    public static class Solid_Http_Zip_SolidHttpRequestExtensions_Responses
    {
        /// <summary>
        /// Returns the content as a GzipStream, caller is responsable for disposing the stream
        /// </summary>
        /// <param name="request">The extended ISolidHttpRequest</param>
        /// <param name="mode">(Optional) The zip archive mode</param>
        /// <returns>An awaitable task</returns>
        public static ValueTask<ZipArchive> AsZipArchive(this ISolidHttpRequest request, ZipArchiveMode? mode = null)
        {
            if (!mode.HasValue)
                return request.As<ZipArchive>();

            return request.As<ZipArchive>((services, content) =>
            {
                var deserializer = services.GetService<ZipArchiveDeserializer>();
                return deserializer.DeserializeAsync(content, mode.Value);
            });
        }
    }
}
