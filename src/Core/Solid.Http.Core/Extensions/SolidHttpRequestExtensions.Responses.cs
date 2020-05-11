using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Solid.Http
{
    /// <summary>
    /// Extension methods that are used to deserialize <see cref="HttpContent" /> into c# classes.
    /// </summary>
    public static class Solid_Http_SolidHttpRequestExtensions_Responses
    {
        /// <summary>
        /// Deserializes <see cref="HttpContent" /> into <typeparamref name="T" />.
        /// </summary>
        /// <typeparam name="T">The type to deserialize as.</typeparam>
        /// <param name="request">The <see cref="ISolidHttpRequest" /> that is being extended.</param>
        /// <returns>A <see cref="ValueTask{T}" /> of type <typeparamref name="T" />.</returns>
        public static ValueTask<T> As<T>(this ISolidHttpRequest request)
        {
            var provider = request.Services.GetService<DeserializerProvider>();
            return request
                .As(content => content.ReadAsAsync<T>(provider))
            ;
        }

        /// <summary>
        /// Deserializes <see cref="HttpContent" /> into <typeparamref name="T" /> using a specified deserialize delegate.
        /// </summary>
        /// <typeparam name="T">The type to deserialize as.</typeparam>
        /// <param name="request">The <see cref="ISolidHttpRequest" /> that is being extended.</param>
        /// <param name="deserialize">A delegate that is used to deserialize <see cref="HttpContent" />.</param>
        /// <returns>A <see cref="ValueTask{T}" /> of type <typeparamref name="T" />.</returns>
        public static ValueTask<T> As<T>(this ISolidHttpRequest request, Func<HttpContent, ValueTask<T>> deserialize)
            => request.As((_, content) => deserialize(content));

        /// <summary>
        /// Deserializes <see cref="HttpContent" /> into an <seealso cref="IEnumerable{T}" /> of type <typeparamref name="T" />.
        /// </summary>
        /// <typeparam name="T">The type to deserialize as.</typeparam>
        /// <param name="request">The <see cref="ISolidHttpRequest" /> that is being extended.</param>
        /// <returns>A <see cref="ValueTask{T}" /> of type <seealso cref="IEnumerable{T}" /> of type <typeparamref name="T" />.</returns>
        public static async ValueTask<IEnumerable<T>> AsMany<T>(this ISolidHttpRequest request)
            => await request.As<IEnumerable<T>>();

        /// <summary>
        /// Returns the <see cref="HttpContent" /> as a <seealso cref="string" />.
        /// </summary>
        /// <param name="request">The <see cref="ISolidHttpRequest" /> that is being extended.</param>
        /// <returns>A <see cref="ValueTask{T}" /> of type <seeaslo cref="string" />.</returns>
        public static ValueTask<string> AsText(this ISolidHttpRequest request)
            => request.As(async content => await content.ReadAsStringAsync());

        /// <summary>
        /// Returns the <see cref="HttpContent" /> as a <seealso cref="Stream" />.
        /// </summary>
        /// <param name="request">The <see cref="ISolidHttpRequest" /> that is being extended.</param>
        /// <returns>A <see cref="ValueTask{T}" /> of type <seeaslo cref="Stream" />.</returns>
        public static ValueTask<Stream> AsStream(this ISolidHttpRequest request)
            => request.As(async content => await content.ReadAsStreamAsync());

        /// <summary>
        /// Returns the <see cref="HttpContent" /> as a <seealso cref="byte" /> array.
        /// </summary>
        /// <param name="request">The <see cref="ISolidHttpRequest" /> that is being extended.</param>
        /// <returns>A <see cref="ValueTask{T}" /> of type <seeaslo cref="byte" /> array.</returns>
        public static ValueTask<byte[]> AsBytes(this ISolidHttpRequest request)
            => request.As(async content => await content.ReadAsByteArrayAsync());
    }
}
