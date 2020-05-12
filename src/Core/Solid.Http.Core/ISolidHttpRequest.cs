using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Solid.Http
{
    /// <summary>
    /// An interface that describes a fluent and awaitable http request.
    /// </summary>
    public interface ISolidHttpRequest
    {
        /// <summary>
        /// The root <see cref="IServiceProvider" /> instance for the application.
        /// </summary>
        IServiceProvider Services { get; }

        /// <summary>
        /// A <see cref="IDictionary{TKey, TValue}" /> of <seealso cref="string" /> and <seealso cref="object" /> that 
        /// can be used by extension methods.
        /// </summary>
        IDictionary<string, object> Context { get; }

        /// <summary>
        /// The <see cref="CancellationToken" /> for the http request.
        /// </summary>
        CancellationToken CancellationToken { get; }

        /// <summary>
        /// The <see cref="HttpRequestMessage" /> that will be sent using an <seealso cref="HttpClient" />.
        /// </summary>
        HttpRequestMessage BaseRequest { get; }

        /// <summary>
        /// The <see cref="HttpResponseMessage" /> that is the result of awaiting the <seealso cref="ISolidHttpRequest" />.
        /// </summary>
        HttpResponseMessage BaseResponse { get; }

        /// <summary>
        /// Registers an event handler that is run just before the <see cref="HttpRequestMessage" /> is sent to the <seealso cref="HttpClient" />.
        /// </summary>
        /// <param name="handler">An event handler that is run just before <see cref="HttpRequestMessage" /> is sent to the <seealso cref="HttpClient" />.</param>
        /// <returns>The <see cref="ISolidHttpRequest" /> so that additional calls can be chained.</returns>
        ISolidHttpRequest OnHttpRequest(Func<IServiceProvider, HttpRequestMessage, ValueTask> handler);
        
        /// <summary>
        /// Registers an event handler that is run just after the <see cref="HttpResponseMessage" /> is receied by the <seealso cref="HttpClient" />.
        /// </summary>
        /// <param name="handler">An event handler that is run just after the <see cref="HttpResponseMessage" /> is receied by the <seealso cref="HttpClient" />.</param>
        /// <returns>The <see cref="ISolidHttpRequest" /> so that additional calls can be chained.</returns>
        ISolidHttpRequest OnHttpResponse(Func<IServiceProvider, HttpResponseMessage, ValueTask> handler);

        /// <summary>
        /// Deserializes the <see cref="HttpContent" /> of the <seealso cref="HttpResponseMessage" /> using the specified delegate.
        /// </summary>
        /// <param name="deserialize">A delegate used to deserialize the <see cref="HttpContent" /> of the <seealso cref="HttpResponseMessage" />.</param>
        /// <typeparam name="T">The type to deserialize to.</typeparam>
        /// <returns>A <see cref="ValueTask{T}" /> of <typeparamref name="T" />.</returns>
        ValueTask<T> As<T>(Func<IServiceProvider, HttpContent, ValueTask<T>> deserialize);

        /// <summary>
        /// Enables the <see cref="ISolidHttpRequest" /> to be awaitable.
        /// </summary>
        /// <returns>A <see cref="TaskAwaiter{T}" /> of <seealso cref="HttpResponseMessage" />.</returns>
        TaskAwaiter<HttpResponseMessage> GetAwaiter();
    }
}
