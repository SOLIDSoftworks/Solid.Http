using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Solid.Http.Providers
{
    public class InstancePerHostHttpClientProvider : HttpClientProvider
    {
        public InstancePerHostHttpClientProvider(IHttpClientFactory factory = null) : base(factory)
        {
        }

        protected override string GenerateHttpClientName(Uri url) => url.Host.ToLower();
    }
}
