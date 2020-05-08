using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Solid.Http.Core.Tests.Stubs
{
    internal class StaticHttpMessageHandler : HttpMessageHandler
    {
        private StaticHttpMessageHandlerOptions _options;

        public StaticHttpMessageHandler(IOptions<StaticHttpMessageHandlerOptions> options)
        {
            _options = options.Value;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = new HttpResponseMessage(_options.StatusCode);
            if(_options.Content != null)
            {
                var stream = new MemoryStream(_options.Content);
                var content = new StreamContent(stream);
                content.Headers.ContentType = new MediaTypeHeaderValue(_options.ContentType);
                if (!string.IsNullOrWhiteSpace(_options.CharSet))
                    content.Headers.ContentType.CharSet = _options.CharSet;
                response.Content = content;
            }
            return Task.FromResult(response);
        }
    }
}
