using System;
using System.Collections.Generic;
using System.Text;

namespace FluentHttp
{
    /// <summary>
    /// Extensions to create a FluentHttpClient with a base address
    /// </summary>
    public static class BaseAddressExtensions
    {
        /// <summary>
        /// Creates a FluentHttpClient with a base address
        /// </summary>
        /// <param name="factory">The IFluentHttpClientFactory</param>
        /// <param name="baseAddress">The base address to use</param>
        /// <returns>FluentHttpClient</returns>
        public static FluentHttpClient CreateWithBaseAddress(this IFluentHttpClientFactory factory, string baseAddress)
        {
            return factory.CreateWithBaseAddress(new Uri(baseAddress));
        }

        /// <summary>
        /// Creates a FluentHttpClient with a base address
        /// </summary>
        /// <param name="factory">The IFluentHttpClientFactory</param>
        /// <param name="baseAddress">The base address to use</param>
        /// <returns>FluentHttpClient</returns>
        public static FluentHttpClient CreateWithBaseAddress(this IFluentHttpClientFactory factory, Uri baseAddress)
        {
            var client = factory.Create();
            client.BaseAddress = baseAddress;
            return client;
        }
    }
}
