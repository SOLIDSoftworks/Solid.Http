using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Net.Http;

namespace Solid.Http
{
    /// <summary>
    /// Extension methods for JSON deserialization.
    /// </summary>
    public static class Solid_Http_Json_SolidHttpRequestExtensions_Responses
    {
        /// <summary>
        /// Deserializes a JSON <see cref="HttpContent" /> as <typeparamref name="T" /> using the 
        /// specified serializer settings.
        /// </summary>
        /// <typeparam name="T">The type of response body.</typeparam>
        /// <param name="request">The <see cref="ISolidHttpRequest" /> that is being extended.</param>
        /// <param name="options">The specified <see cref="JsonSerializerOptions" />.</param>
        /// <returns><see cref="ValueTask{T}" /> of type <typeparamref name="T" /></returns>
        public static async ValueTask<T> As<T>(this ISolidHttpRequest request, JsonSerializerOptions options)
        {
            return await request.As<T>(async content =>
            {
                using (var stream = await content.ReadAsStreamAsync())
                {
                    return await JsonSerializer.DeserializeAsync<T>(stream, options);
                }
            });
        }

        /// <summary>
        /// Deserializes a JSON <see cref="HttpContent" /> as an <seealso cref="IEnumerable{T}" /> 
        /// of type <typeparamref name="T" /> using the specified serializer settings.
        /// </summary>
        /// <typeparam name="T">The type of response body.</typeparam>
        /// <param name="request">The <see cref="ISolidHttpRequest" /> that is being extended.</param>
        /// <param name="options">The specified <see cref="JsonSerializerOptions" />.</param>
        /// <returns><see cref="ValueTask{T}" /> of type <seealso cref="IEnumerable{T}" /> of type <typeparamref name="T" />.</returns>
        public static ValueTask<IEnumerable<T>> AsMany<T>(this ISolidHttpRequest request, JsonSerializerOptions options)
          => request.As<IEnumerable<T>>(options);
    }
}
