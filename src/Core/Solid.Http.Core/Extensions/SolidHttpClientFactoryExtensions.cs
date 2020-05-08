using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Solid.Http
{
    public static class SolidHttpClientFactoryExtensions
    {
        public static ISolidHttpClient CreateWithBaseAddress(this ISolidHttpClientFactory factory, string baseAddress)
        {
            var url = new Uri(baseAddress, UriKind.Absolute);
            return factory.CreateWithBaseAddress(url);
        }
    }
}
