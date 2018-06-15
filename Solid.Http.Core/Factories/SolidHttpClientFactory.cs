using Microsoft.Extensions.Configuration;
using Solid.Http.Abstractions;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;

namespace Solid.Http.Factories
{
    /// <summary>
    /// The SolidHttpClientFactory
    /// </summary>
    internal class SolidHttpClientFactory : ISolidHttpClientFactory
    {
        private ISolidHttpEventInvoker _events;
        private IEnumerable<IDeserializer> _deserializers;
        private IConfiguration _configuration;
        private IHttpClientProvider _provider;

        /// <summary>
        /// The application configuration which can be used in extension methods
        /// </summary>
        public IConfiguration Configuration
        {
            get
            {
                if (_configuration == null)
                {
                    var message =
                        "SolidHttpClientFactory was created with a null configuration." + Environment.NewLine +
                        "If you are initializing a SolidHttpClientFactory manually, please provide an IConfiguration in the constructor of SolidHttpClientFactory." + Environment.NewLine +
                        "If you are initializing using services.AddSolidHttp(), then make sure that your IConfiguration resides in the service container.";
                    throw new InvalidOperationException(message);
                }
                return _configuration;
            }
        }

        /// <summary>
        /// Creates a SolidHttpClientFactory
        /// </summary>
        /// <param name="events">The events to be triggered when a SolidHttpClient is created</param>
        /// <param name="deserializers">The deserializer provider for SolidHttp</param>
        /// <param name="configuration">The application configuration</param>
        public SolidHttpClientFactory(
            ISolidHttpEventInvoker events, 
            IEnumerable<IDeserializer> deserializers, 
            ISolidHttpOptions options, // this is only added so that the ServicePRovider initializes it

            IHttpClientProvider provider, 
            IConfiguration configuration = null)
        {
            _provider = provider;
            _events = events;
            _deserializers = deserializers;
            _configuration = configuration;
        }
        
        /// <summary>
        /// Creates a SolidHttpClient
        /// </summary>
        /// <returns>SolidHttpClient</returns>
        public SolidHttpClient Create()
        {
            return CreateSolidHttpClient(_provider.Get());
        }

        /// <summary>
        /// Releases all resource used by the <see cref="T:SolidHttp.SolidHttpClientFactory"/> object.
        /// </summary>
        /// <remarks>Call <see cref="Dispose"/> when you are finished using the <see cref="T:SolidHttp.SolidHttpClientFactory"/>.
        /// The <see cref="Dispose"/> method leaves the <see cref="T:SolidHttp.SolidHttpClientFactory"/> in an unusable
        /// state. After calling <see cref="Dispose"/>, you must release all references to the
        /// <see cref="T:SolidHttp.SolidHttpClientFactory"/> so the garbage collector can reclaim the memory that the
        /// <see cref="T:SolidHttp.SolidHttpClientFactory"/> was occupying.</remarks>
        public virtual void Dispose()
        {
        }

        private SolidHttpClient CreateSolidHttpClient(HttpClient inner)
        {
            var client = new SolidHttpClient(inner, _deserializers, _events);
            _events.InvokeOnClientCreated(this, client);
            return client;
        }
    }
}
