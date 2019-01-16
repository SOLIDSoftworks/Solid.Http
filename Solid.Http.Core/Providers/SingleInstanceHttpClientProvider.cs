using System;
using System.Net.Http;
using Solid.Http.Abstractions;
using Solid.Http.Factories;

namespace Solid.Http.Providers
{
    internal class SingleInstanceHttpClientProvider : HttpClientProvider
    {
        private static readonly string SingleInstanceName = "Solid.Http";
        public SingleInstanceHttpClientProvider(IHttpClientFactory factory = null) : base(factory)
        {
        }

        protected override string GenerateHttpClientName(Uri url) => SingleInstanceName;
    }
}
