using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Solid.Http.Abstractions
{
    /// <summary>
    /// A SolidHttpClient that is used to perform create SolidHttpRequests. This class is designed be extended using extension methods.
    /// </summary>
    public interface ISolidHttpClient
    {
        /// <summary>
        /// The deserializers this ISolidHttpClient can use
        /// </summary>
        IEnumerable<IDeserializer> Deserializers { get; }

        /// <summary>
        /// Adds a property to the client that can be used in extensions methods
        /// </summary>
        /// <typeparam name="T">The type of parameter</typeparam>
        /// <param name="key">The parameter key</param>
        /// <param name="value">The parameter value</param>
        void AddProperty<T>(string key, T value);

        /// <summary>
        /// Gets a property from the client
        /// </summary>
        /// <typeparam name="T">The type of parameter</typeparam>
        /// <param name="key">The parameter key</param>
        /// <returns>The parameter</returns>
        T GetProperty<T>(string key);

        /// <summary>
        /// Perform an http request
        /// </summary>
        /// <param name="method">The http method for the request</param>
        /// <param name="url">The url to be requested</param>
        /// <param name="cancellationToken">The cancellation token for the request</param>
        /// <returns>An awaitable ISolidHttpRequest</returns>
        ISolidHttpRequest PerformRequestAsync(HttpMethod method, Uri url, CancellationToken cancellationToken);

        /// <summary>
        /// Add a handler to be run when Solid.Http request object is created.
        /// </summary>
        /// <param name="handler">The handler to be run</param>
        /// <returns>ISolidHttpClient</returns>
        ISolidHttpClient OnRequestCreated(Action<IServiceProvider, ISolidHttpRequest> handler);
    }
}
