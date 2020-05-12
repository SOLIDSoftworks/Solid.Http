using Microsoft.Extensions.Options;
using Solid.Http.Zip;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;

namespace Solid.Http
{
    /// <summary>
    /// Extension methods for ZIP deserialization.
    /// </summary>
    public static class Solid_Http_Zip_SolidHttpRequestExtensions_Responses
    {

        /// <summary>
        /// Deserializes an ZIP <see cref="HttpContent" /> as <seealso cref="ZipArchive" />.
        /// </summary>
        /// <param name="request">The <see cref="ISolidHttpRequest" /> that is being extended.</param>
        /// <param name="mode">(Optional) The specified <see cref="ZipArchiveMode" />.</param>
        /// <returns><see cref="ValueTask{T}" /> of type <see cref="ZipArchive" /></returns>
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
