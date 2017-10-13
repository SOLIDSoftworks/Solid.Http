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
            client.AddProperty("Client::BaseAddress", baseAddress);
            client.OnRequestCreated += OnRequestCreated;
            return client;
        }

        private static void OnRequestCreated(object sender, FluentHttpRequestCreatedEventArgs args)
        {
            var baseAddress = args.Request.Client.GetProperty<Uri>("Client::BaseAddress");
            if (baseAddress == null) return;

            var url = new Uri(baseAddress, args.Request.BaseRequest.RequestUri);
            args.Request.BaseRequest.RequestUri = url;
        }
    }
}
