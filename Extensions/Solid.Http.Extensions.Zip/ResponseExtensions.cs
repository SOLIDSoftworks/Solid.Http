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
    public static class ResponseExtensions
    {
        /// <summary>
        /// Returns the content as a GzipStream, caller is responsable for desposing the stream
        /// </summary>
        /// <param name="request"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        public static async Task<T> As<T>(this ISolidHttpRequest request, ZipArchiveMode mode)
        {
            var factory = new ZipArchiveResponseDeserializerFactory(mode);
            var deserialize = factory.CreateDeserializer<T>();
            return await request.As<T>();
        }
    }
}
