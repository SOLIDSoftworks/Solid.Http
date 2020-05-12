using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Solid.Http
{
    /// <summary>
    /// An interface that describes a client that can perform fluent and async http requests.
    /// </summary>
    public interface ISolidHttpClient
    {
        /// <summary>
        /// The base address of the <see cref="ISolidHttpClient" />.
        /// </summary>
        Uri BaseAddress { get; }

        /// <summary>
        /// Registers an event handler that runs when a new <see cref="ISolidHttpRequest" /> is created.
        /// </summary>
        /// <param name="handler">The event handler that runs when a new <see cref="ISolidHttpRequest" /> is created.</param>
        /// <returns>The <see cref="ISolidHttpClient" /> so the additional calls can be chained.</returns>
        ISolidHttpClient OnRequestCreated(Action<IServiceProvider, ISolidHttpRequest> handler);

        /// <summary>
        /// Creates an <see cref="ISolidHttpRequest" /> that is awaitable.
        /// </summary>
        /// <param name="method">The <see cref="HttpMethod" /> of the http request.</param>
        /// <param name="url">The <see cref="Uri" /> of the http request.</param>
        /// <param name="cancellationToken">(Optional) The <see cref="CancellationToken" /> for the http request.</param>
        /// <returns>An <see cref="ISolidHttpRequest" />.</returns>
        ISolidHttpRequest PerformRequestAsync(HttpMethod method, Uri url, CancellationToken cancellationToken = default);
    }
}
