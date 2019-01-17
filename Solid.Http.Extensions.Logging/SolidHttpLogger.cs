using Microsoft.Extensions.Logging;
using Solid.Http.Abstractions;
using Solid.Http.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Solid.Http.Extensions.Logging
{
    internal class SolidHttpLogger : IHttpLogger
    {
        private ILogger<SolidHttpLogger> _inner;

        public SolidHttpLogger(ILogger<SolidHttpLogger> logger)
        {
            _inner = logger;
        }

        public async Task LogRequestAsync(HttpRequestMessage request)
        {
            var scopes = new List<IDisposable>();
            var stopwatch = new Stopwatch();
            scopes.Add(_inner.BeginScope("Solid.Http"));
            scopes.Add(_inner.BeginScope($"{request.Method} - {request.RequestUri}"));
            request.Properties.Add("__Solid.Http.Extensions.Logging::Scopes", scopes);
            request.Properties.Add("__Solid.Http.Extensions.Logging::Stopwatch", stopwatch);

            _inner.LogInformation($"Performing {request.Method} on {request.RequestUri}");
            await LogDebugAsync(request.Headers, request.Content);
            stopwatch.Start();
        }

        public async Task LogResponseAsync(HttpResponseMessage response)
        {
            var stopwatch = response.RequestMessage.Properties["__Solid.Http.Extensions.Logging::Stopwatch"] as Stopwatch;
            var scopes = response.RequestMessage.Properties["__Solid.Http.Extensions.Logging::Scopes"] as IEnumerable<IDisposable>;

            stopwatch.Stop();

            var message = $"{response.RequestMessage.Method} on {response.RequestMessage.RequestUri} took {stopwatch.ElapsedMilliseconds}ms to response and responded with {(int)response.StatusCode}({response.StatusCode})";

            if ((int)response.StatusCode < 400)
                _inner.LogInformation(message);
            else if ((int)response.StatusCode < 500)
                _inner.LogWarning(message);
            else
                _inner.LogError(message);

            await LogDebugAsync(response.Headers, response.Content);

            foreach (var scope in scopes.Reverse())
                scope.Dispose();
        }

        private async Task LogDebugAsync(HttpHeaders headers, HttpContent content)
        {
            var builder = new StringBuilder();
            builder.AppendLine("Headers");
            builder.AppendLine("---------------------------------");
            foreach (var header in headers.Concat(content?.Headers ?? Enumerable.Empty<KeyValuePair<string, IEnumerable<string>>>()))
                foreach (var value in header.Value)
                    builder.AppendLine($"{header.Key}: {value}");
            builder.AppendLine("---------------------------------");
            if (content != null)
            {
                builder.AppendLine("Body");
                builder.AppendLine("---------------------------------");
                using (var stream = new MemoryStream())
                {
                    await content.CopyToAsync(stream);
                    using (var reader = new StreamReader(stream))
                    {
                        var body = await reader.ReadToEndAsync();
                        builder.AppendLine(body);
                    }
                }
                builder.AppendLine("---------------------------------");
            }
            _inner.LogDebug(builder.ToString());
        }
    }
}
