using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Solid.Http
{
    public static class Solid_Http_SolidHttpRequestExtensions_Handlers
    {
        public static ISolidHttpRequest OnHttpRequest(this ISolidHttpRequest request, Func<HttpRequestMessage, ValueTask> handler)
            => request.OnHttpRequest(handler.Convert());
        public static ISolidHttpRequest OnHttpRequest(this ISolidHttpRequest request, Action<HttpRequestMessage> handler)
            => request.OnHttpRequest(handler.Convert());
        public static ISolidHttpRequest OnHttpRequest(this ISolidHttpRequest request, Action<IServiceProvider, HttpRequestMessage> handler)
            => request.OnHttpRequest(handler.Convert());
        public static ISolidHttpRequest OnHttpResponse(this ISolidHttpRequest request, Func<HttpResponseMessage, ValueTask> handler)
            => request.OnHttpResponse(handler.Convert());
        public static ISolidHttpRequest OnHttpResponse(this ISolidHttpRequest request, Action<HttpResponseMessage> handler)
            => request.OnHttpResponse(handler.Convert());
        public static ISolidHttpRequest OnHttpResponse(this ISolidHttpRequest request, Action<IServiceProvider, HttpResponseMessage> handler)
            => request.OnHttpResponse(handler.Convert());

        /// <summary>
        /// Map a handler to a specific http status code
        /// </summary>
        /// <param name="request">The ISolidHttpRequest</param>
        /// <param name="code">The http status code</param>
        /// <param name="handler">The handler</param>
        /// <returns>SolidHttpRequest</returns>
        public static ISolidHttpRequest On(this ISolidHttpRequest request, int code, Action<HttpResponseMessage> handler)
            => request.On((HttpStatusCode)code, handler);

        /// <summary>
        /// Map a handler to a specific http status code
        /// </summary>
        /// <param name="request">The ISolidHttpRequest</param>
        /// <param name="code">The http status code</param>
        /// <param name="handler">The handler</param>
        /// <returns>SolidHttpRequest</returns>
        public static ISolidHttpRequest On(this ISolidHttpRequest request, int code, Action<IServiceProvider, HttpResponseMessage> handler)
            => request.On((HttpStatusCode)code, handler);

        /// <summary>
        /// Map a handler to a specific http status code
        /// </summary>
        /// <param name="request">The ISolidHttpRequest</param>
        /// <param name="code">The http status code</param>
        /// <param name="handler">The handler</param>
        /// <returns>SolidHttpRequest</returns>
        public static ISolidHttpRequest On(this ISolidHttpRequest request, HttpStatusCode code, Action<HttpResponseMessage> handler)
            => request.On(r => r.StatusCode == code, handler);

        /// <summary>
        /// Map a handler to a specific http status code
        /// </summary>
        /// <param name="request">The ISolidHttpRequest</param>
        /// <param name="code">The http status code</param>
        /// <param name="handler">The handler</param>
        /// <returns>SolidHttpRequest</returns>
        public static ISolidHttpRequest On(this ISolidHttpRequest request, HttpStatusCode code, Action<IServiceProvider, HttpResponseMessage> handler)
            => request.On(r => r.StatusCode == code, handler);

        /// <summary>
        /// Map a handler to a specific http status code
        /// </summary>
        /// <param name="request">The ISolidHttpRequest</param>
        /// <param name="predicate">The predicate</param>
        /// <param name="handler">The handler</param>
        /// <returns>SolidHttpRequest</returns>
        public static ISolidHttpRequest On(this ISolidHttpRequest request, Func<HttpResponseMessage, bool> predicate, Action<HttpResponseMessage> handler) 
            => request.On(predicate, (services, response) => handler(response));

        /// <summary>
        /// Map a handler to a specific http status code
        /// </summary>
        /// <param name="request">The ISolidHttpRequest</param>
        /// <param name="predicate">The predicate</param>
        /// <param name="handler">The handler</param>
        /// <returns>SolidHttpRequest</returns>
        public static ISolidHttpRequest On(this ISolidHttpRequest request, Func<HttpResponseMessage, bool> predicate, Action<IServiceProvider, HttpResponseMessage> handler)
        {
            request.OnHttpResponse((services, response) =>
            {
                if (predicate(response))
                    handler(services, response);
            });
            return request;
        }

        /// <summary>
        /// Map an async handler to a specific http status code
        /// </summary>
        /// <param name="request">The ISolidHttpRequest</param>
        /// <param name="code">The http status code</param>
        /// <param name="handler">The async handler</param>
        /// <returns>SolidHttpRequest</returns>
        public static ISolidHttpRequest On(this ISolidHttpRequest request, int code, Func<HttpResponseMessage, ValueTask> handler) 
            => request.On(code, (provider, response) => handler(response));

        /// <summary>
        /// Map an async handler to a specific http status code
        /// </summary>
        /// <param name="request">The ISolidHttpRequest</param>
        /// <param name="code">The http status code</param>
        /// <param name="handler">The async handler</param>
        /// <returns>SolidHttpRequest</returns>
        public static ISolidHttpRequest On(this ISolidHttpRequest request, int code, Func<IServiceProvider, HttpResponseMessage, ValueTask> handler)
            => request.On((HttpStatusCode)code, handler);

        /// <summary>
        /// Map an async handler to a specific http status code
        /// </summary>
        /// <param name="request">The ISolidHttpRequest</param>
        /// <param name="code">The http status code</param>
        /// <param name="handler">The async handler</param>
        /// <returns>SolidHttpRequest</returns>
        public static ISolidHttpRequest On(this ISolidHttpRequest request, HttpStatusCode code, Func<HttpResponseMessage, ValueTask> handler) 
            => request.On(code, (provider, response) => handler(response));

        /// <summary>
        /// Map an async handler to a specific http status code
        /// </summary>
        /// <param name="request">The ISolidHttpRequest</param>
        /// <param name="code">The http status code</param>
        /// <param name="handler">The async handler</param>
        /// <returns>SolidHttpRequest</returns>
        public static ISolidHttpRequest On(this ISolidHttpRequest request, HttpStatusCode code, Func<IServiceProvider, HttpResponseMessage, ValueTask> handler) 
            => request.On(response => response.StatusCode == code, handler);

        /// <summary>
        /// Map an async handler to a specific http status code
        /// </summary>
        /// <param name="request">The ISolidHttpRequest</param>
        /// <param name="predicate">The predicate</param>
        /// <param name="handler">The async handler</param>
        /// <returns>SolidHttpRequest</returns>
        public static ISolidHttpRequest On(this ISolidHttpRequest request, Func<HttpResponseMessage, bool> predicate, Func<HttpResponseMessage, ValueTask> handler) 
            => request.On(predicate, (provider, response) => handler(response));

        /// <summary>
        /// Map an async handler to a specific http status code
        /// </summary>
        /// <param name="request">The ISolidHttpRequest</param>
        /// <param name="predicate">The predicate</param>
        /// <param name="handler">The async handler</param>
        /// <returns>SolidHttpRequest</returns>
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
        /// Expect a success status code
        /// <para>If a non-success status code is received, an InvalidOperationException is thrown</para>
        /// </summary>        
        /// <param name="request">The ISolidHttpRequest</param>
        /// <returns>SolidHttpRequest</returns>
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
