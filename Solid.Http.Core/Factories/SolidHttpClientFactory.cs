using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;

namespace Solid.Http.Factories
{
    /// <summary>
    /// The SolidHttpClientFactory
    /// </summary>
    internal class SolidHttpClientFactory : ISolidHttpClientFactory
    {
        //    private ISolidHttpEvents _events;
        //    private IEnumerable<IDeserializer> _deserializers;
        private IConfiguration _configuration;
        private ISolidHttpEvents _events;
        private IServiceProvider _services;

        //private IHttpClientProvider _provider;

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
            ISolidHttpEvents events, 
            //IEnumerable<IDeserializer> deserializers, 
            ISolidHttpOptions options, // this is only added so that the ServiceProvider initializes it
            IServiceProvider services,
            //IHttpClientProvider provider, 
            IConfiguration configuration = null)
        {
            _events = events;
            _services = services;
            //_provider = provider;
            //_events = events;
            //_deserializers = deserializers;
            _configuration = configuration;
        }
        
        /// <summary>
        /// Creates a SolidHttpClient
        /// </summary>
        /// <returns>SolidHttpClient</returns>
        public ISolidHttpClient Create()
        {
            var client = _services.GetService<ISolidHttpClient>();
            foreach (var handler in _events.ClientCreatedHandlers)
                handler(_services, client);
            return client;
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

        //private SolidHttpClient CreateSolidHttpClient(HttpClient inner)
        //{
        //    var client = new SolidHttpClient(inner, _deserializers, _events);
        //    foreach (var handler in _events.ClientCreatedHandlers)
        //        handler(_services, client);
        //    return client;
        //}
    }
}
