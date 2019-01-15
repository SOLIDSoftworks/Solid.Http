
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Solid.Http.Abstractions
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
        public static ISolidHttpClient CreateWithBaseAddress(this ISolidHttpClientFactory factory, string baseAddress)
        {
            return factory.CreateWithBaseAddress(new Uri(baseAddress));
        }

        /// <summary>
        /// Creates a SolidHttpClient with a base address
        /// </summary>
        /// <param name="factory">The ISolidHttpClientFactory</param>
        /// <param name="baseAddress">The base address to use</param>
        /// <returns>SolidHttpClient</returns>
        public static ISolidHttpClient CreateWithBaseAddress(this ISolidHttpClientFactory factory, Uri baseAddress)
        {
            var client = factory.Create();
            client.AddProperty("Client::BaseAddress", baseAddress);
            client.OnRequestCreated((services, request) => OnRequestCreated(services, request));
            return client;
        }

        private static void OnRequestCreated(IServiceProvider services, ISolidHttpRequest request)
        {
            var baseAddress = request.Client.GetProperty<Uri>("Client::BaseAddress");
            if (baseAddress == null) return;

            var url = new Uri(baseAddress, request.BaseRequest.RequestUri);
            request.BaseRequest.RequestUri = url;
        }
    }
}
