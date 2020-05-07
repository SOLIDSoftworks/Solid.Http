using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace Solid.Http
{
    internal class SolidHttpClient : ISolidHttpClient
    {
        private IServiceProvider _services;
        private SolidHttpOptions _options;

        public SolidHttpClient(IServiceProvider services, IOptions<SolidHttpOptions> options)
        {
            _services = services;
            _options = options.Value;
        }
        //public string Name { get; set; }
        public Uri BaseAddress { get; set; }
        internal Func<IServiceProvider, SolidHttpRequest, ValueTask> OnRequestCreatedAsync { get; set; }

        public ISolidHttpClient OnRequestCreated(Func<IServiceProvider, ISolidHttpRequest, ValueTask> handler)
        {
            OnRequestCreatedAsync = OnRequestCreatedAsync.Add(handler);
            return this;
        }

        public ISolidHttpRequest PerformRequestAsync(HttpMethod method, Uri url, CancellationToken cancellationToken = default)
        {
            var request = _services.GetService<SolidHttpRequest>();
            request.BaseRequest = new HttpRequestMessage(method, url);
            request.BaseRequest.Properties.Add(Constants.BaseAddressKey, BaseAddress);
            request.CancellationToken = cancellationToken;
            request.DeferredRequestCreatedAsync = _options.OnRequestCreatedAsync.Add(OnRequestCreatedAsync);
            return request;
        }
    }
}
