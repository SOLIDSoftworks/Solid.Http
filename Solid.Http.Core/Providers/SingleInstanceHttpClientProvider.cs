using System;
using System.Net.Http;
using Solid.Http.Abstractions;
using Solid.Http.Factories;
using Solid.Http.Models;

namespace Solid.Http.Providers
{
    public class SingleInstanceHttpClientProvider : HttpClientProvider
    {

        public SingleInstanceHttpClientProvider(IHttpClientFactory factory = null) : base(factory)
        {
        }

        protected override string GenerateHttpClientName(Uri url) => "Solid.Http";
    }
}
