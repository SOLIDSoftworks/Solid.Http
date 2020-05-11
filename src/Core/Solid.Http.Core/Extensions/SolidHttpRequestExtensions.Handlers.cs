using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Solid.Http
{
    /// <summary>
    /// Extension methods for adding event handlers to <see cref="ISolidHttpRequest" />.
    /// </summary>
    public static class Solid_Http_SolidHttpRequestExtensions_Handlers
    {
        /// <summary>
        /// Registers an event handler that is run just before the <see cref="HttpRequestMessage" /> is sent to the <seealso cref="HttpClient" />.
        /// </summary>
        /// <param name="request">The <see cref="ISolidHttpRequest" /> that is being extended.</param>
        /// <param name="handler">An event handler that is run just before <see cref="HttpRequestMessage" /> is sent to the <seealso cref="HttpClient" />.</param>
        /// <returns>The <see cref="ISolidHttpRequest" /> so that additional calls can be chained.</returns>
        public static ISolidHttpRequest OnHttpRequest(this ISolidHttpRequest request, Func<HttpRequestMessage, ValueTask> handler)
            => request.OnHttpRequest(handler.Convert());

        /// <summary>
        /// Registers an event handler that is run just before the <see cref="HttpRequestMessage" /> is sent to the <seealso cref="HttpClient" />.
        /// </summary>
        /// <param name="request">The <see cref="ISolidHttpRequest" /> that is being extended.</param>
        /// <param name="handler">An event handler that is run just before <see cref="HttpRequestMessage" /> is sent to the <seealso cref="HttpClient" />.</param>
        /// <returns>The <see cref="ISolidHttpRequest" /> so that additional calls can be chained.</returns>
        public static ISolidHttpRequest OnHttpRequest(this ISolidHttpRequest request, Action<HttpRequestMessage> handler)
            => request.OnHttpRequest(handler.ConvertToAsync());

        /// <summary>
        /// Registers an event handler that is run just before the <see cref="HttpRequestMessage" /> is sent to the <seealso cref="HttpClient" />.
        /// </summary>
        /// <param name="request">The <see cref="ISolidHttpRequest" /> that is being extended.</param>
        /// <param name="handler">An event handler that is run just before <see cref="HttpRequestMessage" /> is sent to the <seealso cref="HttpClient" />.</param>
        /// <returns>The <see cref="ISolidHttpRequest" /> so that additional calls can be chained.</returns>
        public static ISolidHttpRequest OnHttpRequest(this ISolidHttpRequest request, Action<IServiceProvider, HttpRequestMessage> handler)
            => request.OnHttpRequest(handler.ConvertToAsync());

        /// <summary>
        /// Registers an event handler that is run just after the <see cref="HttpResponseMessage" /> is receied by the <seealso cref="HttpClient" />.
        /// </summary>
        /// <param name="request">The <see cref="ISolidHttpRequest" /> that is being extended.</param>
        /// <param name="handler">An event handler that is run just after the <see cref="HttpResponseMessage" /> is received by the <seealso cref="HttpClient" />.</param>
        /// <returns>The <see cref="ISolidHttpRequest" /> so that additional calls can be chained.</returns>
        public static ISolidHttpRequest OnHttpResponse(this ISolidHttpRequest request, Func<HttpResponseMessage, ValueTask> handler)
            => request.OnHttpResponse(handler.Convert());

        /// <summary>
        /// Registers an event handler that is run just after the <see cref="HttpResponseMessage" /> is receied by the <seealso cref="HttpClient" />.
        /// </summary>
        /// <param name="request">The <see cref="ISolidHttpRequest" /> that is being extended.</param>
        /// <param name="handler">An event handler that is run just after the <see cref="HttpResponseMessage" /> is receied by the <seealso cref="HttpClient" />.</param>
        /// <returns>The <see cref="ISolidHttpRequest" /> so that additional calls can be chained.</returns>
        public static ISolidHttpRequest OnHttpResponse(this ISolidHttpRequest request, Action<HttpResponseMessage> handler)
            => request.OnHttpResponse(handler.ConvertToAsync());

        /// <summary>
        /// Registers an event handler that is run just after the <see cref="HttpResponseMessage" /> is receied by the <seealso cref="HttpClient" />.
        /// </summary>
        /// <param name="request">The <see cref="ISolidHttpRequest" /> that is being extended.</param>
        /// <param name="handler">An event handler that is run just after the <see cref="HttpResponseMessage" /> is receied by the <seealso cref="HttpClient" />.</param>
        /// <returns>The <see cref="ISolidHttpRequest" /> so that additional calls can be chained.</returns>
        public static ISolidHttpRequest OnHttpResponse(this ISolidHttpRequest request, Action<IServiceProvider, HttpResponseMessage> handler)
            => request.OnHttpResponse(handler.ConvertToAsync());

        /// <summary>
        /// Map a handler to a specific http status code.
        /// </summary>
        /// <param name="request">The <see cref="ISolidHttpRequest" /> that is being extended.</param>
        /// <param name="code">The http status code.</param>
        /// <param name="handler">The handler to run when the status code of the <see cref="HttpResponseMessage" /> matches <paramref name="code" />.</param>
        /// <returns>The <see cref="ISolidHttpRequest" /> so that additional calls can be chained.</returns>
        public static ISolidHttpRequest On(this ISolidHttpRequest request, int code, Action<HttpResponseMessage> handler)
            => request.On((HttpStatusCode)code, handler);

        /// <summary>
        /// Map a handler to a specific http status code.
        /// </summary>
        /// <param name="request">The <see cref="ISolidHttpRequest" /> that is being extended.</param>
        /// <param name="code">The http status code.</param>
        /// <param name="handler">The handler to run when the status code of the <see cref="HttpResponseMessage" /> matches <paramref name="code" />.</param>
        /// <returns>The <see cref="ISolidHttpRequest" /> so that additional calls can be chained.</returns>
        public static ISolidHttpRequest On(this ISolidHttpRequest request, int code, Action<IServiceProvider, HttpResponseMessage> handler)
            => request.On((HttpStatusCode)code, handler);

        /// <summary>
        /// Map a handler to a specific http status code.
        /// </summary>
        /// <param name="request">The <see cref="ISolidHttpRequest" /> that is being extended.</param>
        /// <param name="code">The http status code.</param>
        /// <param name="handler">The handler to run when the status code of the <see cref="HttpResponseMessage" /> matches <paramref name="code" />.</param>
        /// <returns>The <see cref="ISolidHttpRequest" /> so that additional calls can be chained.</returns>
        public static ISolidHttpRequest On(this ISolidHttpRequest request, HttpStatusCode code, Action<HttpResponseMessage> handler)
            => request.On(r => r.StatusCode == code, handler);

        /// <summary>
        /// Map a handler to a specific http status code.
        /// </summary>
        /// <param name="request">The <see cref="ISolidHttpRequest" /> that is being extended.</param>
        /// <param name="code">The http status code.</param>
        /// <param name="handler">The handler to run when the status code of the <see cref="HttpResponseMessage" /> matches <paramref name="code" />.</param>
        /// <returns>The <see cref="ISolidHttpRequest" /> so that additional calls can be chained.</returns>
        public static ISolidHttpRequest On(this ISolidHttpRequest request, HttpStatusCode code, Action<IServiceProvider, HttpResponseMessage> handler)
            => request.On(r => r.StatusCode == code, handler);

        /// <summary>
        /// Map a handler to a specific http status code.
        /// </summary>
        /// <param name="request">The <see cref="ISolidHttpRequest" /> that is being extended.</param>
        /// <param name="code">The http status code.</param>
        /// <param name="handler">The handler to run when the status code of the <see cref="HttpResponseMessage" /> matches <paramref name="code" />.</param>
        /// <returns>The <see cref="ISolidHttpRequest" /> so that additional calls can be chained.</returns>
        public static ISolidHttpRequest On(this ISolidHttpRequest request, int code, Func<HttpResponseMessage, ValueTask> handler) 
            => request.On(code, handler.Convert());

        /// <summary>
        /// Map a handler to a specific http status code.
        /// </summary>
        /// <param name="request">The <see cref="ISolidHttpRequest" /> that is being extended.</param>
        /// <param name="code">The http status code.</param>
        /// <param name="handler">The handler to run when the status code of the <see cref="HttpResponseMessage" /> matches <paramref name="code" />.</param>
        /// <returns>The <see cref="ISolidHttpRequest" /> so that additional calls can be chained.</returns>
        public static ISolidHttpRequest On(this ISolidHttpRequest request, int code, Func<IServiceProvider, HttpResponseMessage, ValueTask> handler)
            => request.On((HttpStatusCode)code, handler);

        /// <summary>
        /// Map a handler to a specific http status code.
        /// </summary>
        /// <param name="request">The <see cref="ISolidHttpRequest" /> that is being extended.</param>
        /// <param name="code">The http status code.</param>
        /// <param name="handler">The handler to run when the status code of the <see cref="HttpResponseMessage" /> matches <paramref name="code" />.</param>
        /// <returns>The <see cref="ISolidHttpRequest" /> so that additional calls can be chained.</returns>
        public static ISolidHttpRequest On(this ISolidHttpRequest request, HttpStatusCode code, Func<HttpResponseMessage, ValueTask> handler) 
            => request.On(code, handler.Convert());

        /// <summary>
        /// Map a handler to a specific http status code.
        /// </summary>
        /// <param name="request">The <see cref="ISolidHttpRequest" /> that is being extended.</param>
        /// <param name="code">The http status code.</param>
        /// <param name="handler">The handler to run when the status code of the <see cref="HttpResponseMessage" /> matches <paramref name="code" />.</param>
        /// <returns>The <see cref="ISolidHttpRequest" /> so that additional calls can be chained.</returns>
        public static ISolidHttpRequest On(this ISolidHttpRequest request, HttpStatusCode code, Func<IServiceProvider, HttpResponseMessage, ValueTask> handler) 
            => request.On(response => response.StatusCode == code, handler);

        /// <summary>
        /// Map a handler that is run on a specific condition defined by <paramref name="predicate" />.
        /// </summary>
        /// <param name="request">The <see cref="ISolidHttpRequest" /> that is being extended.</param>
        /// <param name="predicate">The predicate</param>
        /// <param name="handler">The handler to run when <paramref name ="predicate" /> returns true.</param>
        /// <returns>The <see cref="ISolidHttpRequest" /> so that additional calls can be chained.</returns>
        public static ISolidHttpRequest On(this ISolidHttpRequest request, Func<HttpResponseMessage, bool> predicate, Action<HttpResponseMessage> handler) 
            => request.On(predicate, handler.ConvertToAsync());

        /// <summary>
        /// Map a handler that is run on a specific condition defined by <paramref name="predicate" />.
        /// </summary>
        /// <param name="request">The <see cref="ISolidHttpRequest" /> that is being extended.</param>
        /// <param name="predicate">The predicate</param>
        /// <param name="handler">The handler to run when <paramref name ="predicate" /> returns true.</param>
        /// <returns>The <see cref="ISolidHttpRequest" /> so that additional calls can be chained.</returns>
        public static ISolidHttpRequest On(this ISolidHttpRequest request, Func<HttpResponseMessage, bool> predicate, Action<IServiceProvider, HttpResponseMessage> handler)
            => request.On(predicate, handler.ConvertToAsync());

        /// <summary>
        /// Map a handler that is run on a specific condition defined by <paramref name="predicate" />.
        /// </summary>
        /// <param name="request">The <see cref="ISolidHttpRequest" /> that is being extended.</param>
        /// <param name="predicate">The predicate</param>
        /// <param name="handler">The handler to run when <paramref name ="predicate" /> returns true.</param>
        /// <returns>The <see cref="ISolidHttpRequest" /> so that additional calls can be chained.</returns>
        public static ISolidHttpRequest On(this ISolidHttpRequest request, Func<HttpResponseMessage, bool> predicate, Func<HttpResponseMessage, ValueTask> handler) 
            => request.On(predicate, handler.Convert());

        /// <summary>
        /// Map a handler that is run on a specific condition defined by <paramref name="predicate" />.
        /// </summary>
        /// <param name="request">The <see cref="ISolidHttpRequest" /> that is being extended.</param>
        /// <param name="predicate">The predicate</param>
        /// <param name="handler">The handler to run when <paramref name ="predicate" /> returns true.</param>
        /// <returns>The <see cref="ISolidHttpRequest" /> so that additional calls can be chained.</returns>
        public static ISolidHttpRequest On(this ISolidHttpRequest request, Func<HttpResponseMessage, bool> predicate, Func<IServiceProvider, HttpResponseMessage, ValueTask> handler)
        {
            request.OnHttpResponse(async (services, response) =>
            {
                if (predicate(response))
                    await handler(services, response);
            });
            return request;
        }

        /// <summary>
        /// Expect a success status code.
        /// <para>If a status code  in the 400 range is received, a <see cref="ClientErrorException" /> is thrown.</para>
        /// <para>If a status code  in the 500 range is received, a <see cref="ServerErrorException" /> is thrown.</para>
        /// </summary>        
        /// <param name="request">The <see cref="ISolidHttpRequest" /> that is being extended.</param>
        /// <returns>The <see cref="ISolidHttpRequest" /> so that additional calls can be chained.</returns>
        public static ISolidHttpRequest ExpectSuccess(this ISolidHttpRequest request)
        {
            request.OnHttpResponse(async (services, response) =>
            {
                if (!response.IsSuccessStatusCode)
                {
                    var message = await GenerateNonSuccessMessage(response);
                    if ((int)response.StatusCode < 500)
                        throw new ClientErrorException(message);
                    else
                        throw new ServerErrorException(message);
                }
            });
            return request;
        }

        /// <summary>
        /// Sets the request to ignores serialization errors from <see cref="IDeserializer" /> and return null instead.
        /// </summary>
        /// <param name="request">The <see cref="ISolidHttpRequest" /> that is being extended.</param>
        /// <returns>The <see cref="ISolidHttpRequest" /> so that additional calls can be chained.</returns>
        public static ISolidHttpRequest IgnoreSerializationError(this ISolidHttpRequest request)
        {
            request.Context.Add(Constants.IgnoreSerializationErrorKey, true);
            return request;
        }

        private static async ValueTask<string> GenerateNonSuccessMessage(HttpResponseMessage response)
        {
            var builder = new StringBuilder();
            builder.AppendLine($"Expected success status code. Got { (int)response.StatusCode } ({response.StatusCode.ToString()})");

            if (response.Content != null)
            {
                var content = await response.Content.ReadAsStringAsync();
                if (!string.IsNullOrEmpty(content))
                    builder.AppendLine($"Content in the response was: {content}");
            }

            return builder.ToString();
        }
    }
}
