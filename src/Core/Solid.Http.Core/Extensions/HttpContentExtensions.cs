using Solid.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace System.Net.Http
{
    internal static class HttpContentExtensions
    {
        public static async ValueTask<T> ReadAsAsync<T>(this HttpContent content, DeserializerProvider provider)
        {
            if (content == null) return default;

            var type = typeof(T);
            if (type == typeof(string)) return (T)(object)await content.ReadAsStringAsync();
            if (type == typeof(byte[])) return (T)(object)await content.ReadAsByteArrayAsync();
            if (type == typeof(Stream)) return (T)(object)await content.ReadAsStreamAsync();

            var deserializer = provider.GetDeserializer(content.Headers.ContentType, typeof(T));
            if (deserializer == null) return default;
            return await deserializer.DeserializeAsync<T>(content);
        }
    }
}
