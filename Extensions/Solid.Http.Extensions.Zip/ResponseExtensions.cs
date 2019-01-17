using Solid.Http.Extenstions.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Solid.Http.Abstractions
{
    /// <summary>
    /// ResponseExtensions
    /// </summary>
    public static class ResponseExtensions
    {
        /// <summary>
        /// Returns the content as a GzipStream, caller is responsable for disposing the stream
        /// </summary>
        /// <param name="request">The extended ISolidHttpRequest</param>
        /// <param name="mode">The zip archive mode</param>
        /// <returns>An awaitable task</returns>
        public static Task<ZipArchive> AsZipArchive(this ISolidHttpRequest request, ZipArchiveMode mode = ZipArchiveMode.Read)
        {
            var factory = new ZipArchiveResponseDeserializerFactory(mode);
            var deserialize = factory.CreateDeserializer<ZipArchive>();
            return request.As<ZipArchive>();
        }
    }
}
