using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Solid.Http
{
    public static class Solid_Http_SolidHttpRequestExtensions_Responses
    {
        /// <summary>
        /// Deserializes the response content using a specified deserializer
        /// </summary>
        /// <typeparam name="T">The type to deserialize to</typeparam>
        /// <param name="request">The ISolidHttpRequest</param>
        /// <param name="deserialize">The deserialization method</param>
        /// <returns>Task of type T</returns>
        public static ValueTask<T> As<T>(this ISolidHttpRequest request)
        {
            var provider = null as DeserializerProvider;
            return request
                .OnHttpResponse((services, _) => provider = services.GetRequiredService<DeserializerProvider>())
                .As(content => content.ReadAsAsync<T>(provider))
            ;
        }

        public static ValueTask<T> As<T>(this ISolidHttpRequest request, Func<HttpContent, ValueTask<T>> deserialize)
            => request.As((_, content) => deserialize(content));


        /// <summary>
        /// Deserializes the response content as an array of type T
        /// </summary>
        /// <typeparam name="T">The type to deserialize to</typeparam>
        /// <param name="request">The ISolidHttpRequest</param>
        /// <returns>Task of type IEnumerable&lt;T&gt;</returns>
        public static async ValueTask<IEnumerable<T>> AsMany<T>(this ISolidHttpRequest request)
            => await request.As<IEnumerable<T>>();

        /// <summary>
        /// Returns the response content as text
        /// </summary>
        /// <param name="request">The ISolidHttpRequest</param>
        /// <returns>Task of type string</returns>
        public static ValueTask<string> AsText(this ISolidHttpRequest request)
            => request.As(async content => await content.ReadAsStringAsync());

        /// <summary>
        /// Returns the response content as a Stream
        /// </summary>
        /// <param name="request">The ISolidHttpRequest</param>
        /// <returns>Task of type Stream</returns>
        public static ValueTask<Stream> AsStream(this ISolidHttpRequest request)
            => request.As(async content => await content.ReadAsStreamAsync());

        /// <summary>
        /// Returns the response content as bytes
        /// </summary>
        /// <param name="request">The ISolidHttpRequest</param>
        /// <returns>Task of type byte[]</returns>
        public static ValueTask<byte[]> AsBytes(this ISolidHttpRequest request)
            => request.As(async content => await content.ReadAsByteArrayAsync());
    }
}
