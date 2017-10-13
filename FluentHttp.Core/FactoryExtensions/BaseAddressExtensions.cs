using System;
using System.Collections.Generic;
using System.Text;

namespace FluentHttp
{
    public static class BaseAddressExtensions
    {

        public static FluentHttpClient CreateWithBaseAddress(this IFluentHttpClientFactory factory, string baseAddress)
        {
            return factory.CreateWithBaseAddress(new Uri(baseAddress));
        }

        public static FluentHttpClient CreateWithBaseAddress(this IFluentHttpClientFactory factory, Uri baseAddress)
        {
            var client = factory.Create();
            client.BaseAddress = baseAddress;
            return client;
        }
    }
}
