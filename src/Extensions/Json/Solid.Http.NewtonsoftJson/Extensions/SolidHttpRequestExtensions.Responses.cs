using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace Solid.Http
{
    /// <summary>
    /// Extension methods for JSON deserialization.
    /// </summary>
    public static class Solid_Http_NewtonsoftJson_SolidHttpRequestExtensions_Responses
    {
        /// <summary>
        /// Deserializes a JSON <see cref="HttpContent" /> as <typeparamref name="T" /> using the 
        /// specified serializer settings.
        /// </summary>
        /// <typeparam name="T">The type of response body.</typeparam>
        /// <param name="request">The <see cref="ISolidHttpRequest" /> that is being extended.</param>
        /// <param name="settings">The specified <see cref="JsonSerializerSettings" />.</param>
        /// <returns><see cref="ValueTask{T}" /> of type <typeparamref name="T" /></returns>
        public static async ValueTask<T> As<T>(this ISolidHttpRequest request, JsonSerializerSettings settings)
        {
            return await request.As<T>(async content =>
            {
                var json = await content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(json, settings);
            });
        }

        /// <summary>
        /// Deserializes a JSON <see cref="HttpContent" /> as an anonymous type <typeparamref name="T" /> using the 
        /// specified serializer settings.
        /// </summary>
        /// <typeparam name="T">The anonymous type of response body.</typeparam>
        /// <param name="request">The <see cref="ISolidHttpRequest" /> that is being extended.</param>
        /// <param name="anonymous">An object of type <typeparamref name="T" /> that provides a schema for deserialization.</param>
        /// <param name="settings">The specified <see cref="JsonSerializerSettings" />.</param>
        /// <returns><see cref="ValueTask{T}" /> of type <typeparamref name="T" /></returns>
        public static ValueTask<T> As<T>(this ISolidHttpRequest request, T anonymous, JsonSerializerSettings settings) 
            => request.As<T>(settings);

        /// <summary>
        /// Deserializes a JSON <see cref="HttpContent" /> as an <seealso cref="IEnumerable{T}" /> 
        /// of type <typeparamref name="T" /> using the specified serializer settings.
        /// </summary>
        /// <typeparam name="T">The type of response body.</typeparam>
        /// <param name="request">The <see cref="ISolidHttpRequest" /> that is being extended.</param>
        /// <param name="settings">The specified <see cref="JsonSerializerSettings" />.</param>
        /// <returns><see cref="ValueTask{T}" /> of type <seealso cref="IEnumerable{T}" /> of type <typeparamref name="T" />.</returns>
        public static ValueTask<IEnumerable<T>> AsMany<T>(this ISolidHttpRequest request, JsonSerializerSettings settings) 
            => request.As<IEnumerable<T>>(settings);

        /// <summary>
        /// Deserializes a JSON <see cref="HttpContent" /> as an <seealso cref="IEnumerable{T}" /> 
        /// of type <typeparamref name="T" /> using the specified serializer settings.
        /// </summary>
        /// <typeparam name="T">The type of response body.</typeparam>
        /// <param name="request">The <see cref="ISolidHttpRequest" /> that is being extended.</param>
        /// <param name="anonymous">An object of type <typeparamref name="T" /> that provides a schema for deserialization.</param>
        /// <param name="settings">The specified <see cref="JsonSerializerSettings" />.</param>
        /// <returns><see cref="ValueTask{T}" /> of type <seealso cref="IEnumerable{T}" /> of type <typeparamref name="T" />.</returns>
        public static ValueTask<IEnumerable<T>> AsMany<T>(this ISolidHttpRequest request, T anonymous, JsonSerializerSettings settings) 
            => request.As<IEnumerable<T>>(settings);
    }
}
