using Solid.Http.Abstractions;
using Solid.Http.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Solid.Http
{
    /// <summary>
    /// Extensions to create a SolidHttpClient with a base address
    /// </summary>
    public static class BaseAddressExtensions
    {
        /// <summary>
        /// Creates a SolidHttpClient with a base address
        /// </summary>
        /// <param name="factory">The ISolidHttpClientFactory</param>
        /// <param name="baseAddress">The base address to use</param>
        /// <returns>SolidHttpClient</returns>
        public static SolidHttpClient CreateWithBaseAddress(this ISolidHttpClientFactory factory, string baseAddress)
        {
            return factory.CreateWithBaseAddress(new Uri(baseAddress));
        }

        /// <summary>
        /// Creates a SolidHttpClient with a base address
        /// </summary>
        /// <param name="factory">The ISolidHttpClientFactory</param>
        /// <param name="baseAddress">The base address to use</param>
        /// <returns>SolidHttpClient</returns>
        public static SolidHttpClient CreateWithBaseAddress(this ISolidHttpClientFactory factory, Uri baseAddress)
        {
            var client = factory.Create();
            client.AddProperty("Client::BaseAddress", baseAddress);
            client.OnRequestCreated += OnRequestCreated;
            return client;
        }

        private static void OnRequestCreated(object sender, SolidHttpRequestCreatedEventArgs args)
        {
            var baseAddress = args.Request.Client.GetProperty<Uri>("Client::BaseAddress");
            if (baseAddress == null) return;

            var url = new Uri(baseAddress, args.Request.BaseRequest.RequestUri);
            args.Request.BaseRequest.RequestUri = url;
        }
    }
}
