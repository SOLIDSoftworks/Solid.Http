using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Solid.Http.Abstractions;
using Solid.Http.Events;

namespace Solid.Http.Factories
{
    /// <summary>
    /// The SolidHttpClientFactory
    /// </summary>
    internal class SolidHttpClientFactory : ISolidHttpClientFactory
    {
        private IConfiguration _configuration;
        private IServiceProvider _services;
        private Action<IServiceProvider, ISolidHttpClient> _onClientCreated;

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
        /// <param name="services">The root service provider</param>
        /// <param name="onClientCreated">The registered onClientCreated handlers</param>
        /// <param name="configuration">The application configuration</param>
        public SolidHttpClientFactory(
            IServiceProvider services,
            SolidEventHandler<ISolidHttpClient> onClientCreated,
            IConfiguration configuration = null)
        {
            _services = services;
            _onClientCreated += onClientCreated.Handler ?? onClientCreated.Noop;
            _configuration = configuration;
        }
        
        /// <summary>
        /// Creates a SolidHttpClient
        /// </summary>
        /// <returns>SolidHttpClient</returns>
        public ISolidHttpClient Create()
        {
            var client = _services.GetService<ISolidHttpClient>();
            _onClientCreated(_services, client);
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
    }
}
