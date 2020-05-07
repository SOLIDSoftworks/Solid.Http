using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Solid.Http
{
    internal class SolidHttpClientFactory : ISolidHttpClientFactory, IDisposable
    {
        private IServiceProvider _services;
        private ILogger<SolidHttpClientFactory> _logger;
        private IDisposable _optionsChangeToken;

        public SolidHttpOptions Options { get; private set; }

        public SolidHttpClientFactory(IServiceProvider services, ILogger<SolidHttpClientFactory> logger, IOptionsMonitor<SolidHttpOptions> monitor)
        {
            _services = services;
            _logger = logger;
            Options = monitor.CurrentValue;
            _optionsChangeToken = monitor.OnChange((options, _) => Options = options);
        }

        public async ValueTask<ISolidHttpClient> CreateAsync()
        {
            _logger.LogDebug($"Creating { nameof(SolidHttpClient) }");
            var client = _services.GetService<SolidHttpClient>();
            await Options.OnClientCreatedAsync.InvokeAllAsync(_services, client);
            return client;
        }

        /// <summary>
        /// Creates a SolidHttpClient with a base address
        /// </summary>
        /// <param name="baseAddress">The base address to use</param>
        /// <returns>SolidHttpClient</returns>
        public async ValueTask<ISolidHttpClient> CreateWithBaseAddressAsync(Uri baseAddress)
        {
            var client = await CreateAsync();
            if (baseAddress == null) throw new ArgumentNullException(nameof(baseAddress));
            if (!string.IsNullOrEmpty(baseAddress.Query)) throw new ArgumentException("BaseAddresses with query parameters not supported.", nameof(baseAddress));
            client.BaseAddress = baseAddress.WithTrailingSlash();
            client.OnRequestCreated((_, request) =>
            {
                if (!request.Context.TryGetValue<Uri>(Constants.BaseAddressKey, out var b)) return;

                var url = new Uri(b, request.BaseRequest.RequestUri);
                request.BaseRequest.RequestUri = url;
            });
            return client;
        }

        public void Dispose()
        {
            _optionsChangeToken?.Dispose();
        }
    }
}
