using Solid.Http.Zip;
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
        public static async Task<T> As<T>(this ISolidHttpRequest request, ZipArchiveMode mode)
        {
            var factory = new ZipArchiveResponseDeserializerFactory(mode);
            var deserialize = factory.CreateDeserializer<T>();
            return await request.As<T>();
        }
    }
}
