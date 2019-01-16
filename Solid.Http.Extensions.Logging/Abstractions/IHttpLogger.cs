using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Solid.Http.Extensions.Logging.Abstractions
{
    /// <summary>
    /// An abstraction to perform logging on http request and response
    /// </summary>
    public interface IHttpLogger
    {
        /// <summary>
        /// Logs the http request
        /// </summary>
        /// <param name="request">The http request to be logged</param>
        /// <returns>An awaitable task</returns>
        Task LogRequestAsync(HttpRequestMessage request);

        /// <summary>
        /// Logs the http response
        /// </summary>
        /// <param name="response">The http response to be logged</param>
        /// <returns>An awaitable task</returns>
        Task LogResponseAsync(HttpResponseMessage response);
    }
}
