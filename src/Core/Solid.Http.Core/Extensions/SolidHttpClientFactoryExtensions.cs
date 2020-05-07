using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Solid.Http
{
    public static class SolidHttpClientFactoryExtensions
    {
        public static ValueTask<ISolidHttpClient> CreateWithBaseAddressAsync(this ISolidHttpClientFactory factory, string baseAddress)
        {
            var url = new Uri(baseAddress, UriKind.Absolute);
            return factory.CreateWithBaseAddressAsync(url);
        }
    }
}
