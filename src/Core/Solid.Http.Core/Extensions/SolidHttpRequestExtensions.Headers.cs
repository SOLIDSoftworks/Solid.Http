using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;

namespace Solid.Http
{
    /// <summary>
    /// Extension methods for adding headers to <see cref="ISolidHttpRequest" />.
    /// </summary>
    public static class Solid_Http_SolidHttpRequestExtensions_Headers
    {
        /// <summary>
        /// Adds a header to the <see cref="ISolidHttpRequest" />.
        /// </summary>
        /// <param name="request">The <see cref="ISolidHttpRequest" /> that is being extended.</param>
        /// <param name="name">The name of the header.</param>
        /// <param name="values">The value(s) for the header.</param>
        /// <returns>The <see cref="ISolidHttpRequest" /> so that additional calls can be chained.</returns>
        public static ISolidHttpRequest WithHeader(this ISolidHttpRequest request, string name, StringValues values)
            => request.WithHeaders(headers => headers.Add(name, values.ToArray()));

        /// <summary>
        /// Adds a header to the <see cref="ISolidHttpRequest" />.
        /// </summary>
        /// <param name="request">The <see cref="ISolidHttpRequest" /> that is being extended.</param>
        /// <param name="name">The name of the header.</param>
        /// <param name="value">The value of the header.</param>
        /// <returns>The <see cref="ISolidHttpRequest" /> so that additional calls can be chained.</returns>
        public static ISolidHttpRequest WithHeader(this ISolidHttpRequest request, string name, object value)
            => request.WithHeaders(headers => headers.Add(name, value.ConvertToStrings()));

        /// <summary>
        /// Adds a header to the <see cref="ISolidHttpRequest" />.
        /// </summary>
        /// <param name="request">The <see cref="ISolidHttpRequest" /> that is being extended.</param>
        /// <param name="name">The name of the header.</param>
        /// <param name="firstValue">The first value of the header.</param>
        /// <param name="secondValue">The second value of the header.</param>
        /// <param name="moreValues">More values for the header.</param>
        /// <returns>The <see cref="ISolidHttpRequest" /> so that additional calls can be chained.</returns>
        public static ISolidHttpRequest WithHeader(this ISolidHttpRequest request, string name, object firstValue, object secondValue, params object[] moreValues)
        {
            var values = new[] { firstValue, secondValue }.Concat(moreValues).SelectMany(o => o.ConvertToStrings());
            return request.WithHeaders(headers => headers.Add(name, values));
        }

        /// <summary>
        /// Adds a header to the <see cref="ISolidHttpRequest" /> using a delegate.
        /// </summary>
        /// <param name="request">The <see cref="ISolidHttpRequest" /> that is being extended.</param>
        /// <param name="addHeaders">The delegate used to add headers to <see cref="HttpRequestHeaders" />.</param>
        /// <returns>The <see cref="ISolidHttpRequest" /> so that additional calls can be chained.</returns>
        public static ISolidHttpRequest WithHeaders(this ISolidHttpRequest request, Action<HttpRequestHeaders> addHeaders)
        {
            addHeaders(request.BaseRequest.Headers);
            return request;
        }


        /// <summary>
        /// Adds headers to the <see cref="ISolidHttpRequest"/>.
        /// </summary>
        /// <param name="request">The <see cref="ISolidHttpRequest"/> that is being extended.</param>
        /// <param name="parameters">The headers to be added.</param>
        /// <returns>The <see cref="ISolidHttpRequest"/> so that additional calls can be chained.</returns>
        public static ISolidHttpRequest WithHeaders(this ISolidHttpRequest request, IDictionary<string, object> parameters)
        {
            foreach (var parameter in parameters)
                request.WithHeader(parameter.Key, parameter.Value);
            return request;
        }

        /// <summary>
        /// Adds headers to the <see cref="ISolidHttpRequest"/>.
        /// </summary>
        /// <param name="request">The <see cref="ISolidHttpRequest"/> that is being extended.</param>
        /// <param name="parameters">The headers to be added.</param>
        /// <returns>The <see cref="ISolidHttpRequest"/> so that additional calls can be chained.</returns>
        public static ISolidHttpRequest WithHeaders(this ISolidHttpRequest request, IDictionary<string, string> parameters)
        {
            foreach (var parameter in parameters)
                request.WithHeader(parameter.Key, parameter.Value);
            return request;
        }

        /// <summary>
        /// Adds an authorization header to the <see cref="ISolidHttpRequest"/>.
        /// </summary>
        /// <param name="request">The <see cref="ISolidHttpRequest" /> that is being extended.</param>
        /// <param name="scheme">The authorization scheme of the header value.</param>
        /// <param name="value">The authorization value.</param>
        /// <returns>The <see cref="ISolidHttpRequest" /> so that additional calls can be chained.</returns>
        public static ISolidHttpRequest WithAuthorizationHeader(this ISolidHttpRequest request, string scheme, string value)
            => request.WithHeader("Authorization", $"{scheme} {value}");

        /// <summary>
        /// Adds an authorization header to the <see cref="ISolidHttpRequest"/>.
        /// </summary>
        /// <param name="request">The <see cref="ISolidHttpRequest" /> that is being extended.</param>
        /// <param name="token">The token to use in the authorization header.</param>
        /// <returns>The <see cref="ISolidHttpRequest" /> so that additional calls can be chained.</returns>
        public static ISolidHttpRequest WithBearerAuthorizationHeader(this ISolidHttpRequest request, string token)
            => request.WithAuthorizationHeader("Bearer", token);

        /// <summary>
        /// Adds an authorization header to the <see cref="ISolidHttpRequest"/>.
        /// </summary>
        /// <param name="request">The <see cref="ISolidHttpRequest" /> that is being extended.</param>
        /// <param name="userName">The user name to be used in Basic authentication.</param>
        /// <param name="password">The password to be used in Basic authentication.</param>
        /// <returns>The <see cref="ISolidHttpRequest" /> so that additional calls can be chained.</returns>
        public static ISolidHttpRequest WithBasicAuthorizationHeader(this ISolidHttpRequest request, string userName, string password)
            => request.WithAuthorizationHeader("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes($"{userName}:{password}")));
    }
}
