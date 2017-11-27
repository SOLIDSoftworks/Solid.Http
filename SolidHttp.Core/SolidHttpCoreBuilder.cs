using Microsoft.Extensions.DependencyInjection;
using Solid.Http.Abstractions;
using Solid.Http.Cache;
using Solid.Http.Events;
using Solid.Http.Factories;
using Solid.Http.Setup;
using System;
using System.Collections.Generic;
using System.Text;

namespace Solid.Http
{
    public class SolidHttpCoreBuilder : SolidHttpCoreBuilder<SimpleHttpClientFactory>
    {
        public SolidHttpCoreBuilder()
        {
        }

        public SolidHttpCoreBuilder(IServiceCollection services) 
            : base(services)
        {
        }
    }

    public class SolidHttpCoreBuilder<TFactory> : ISolidHttpCoreBuilder, IDisposable
        where TFactory : class, IHttpClientFactory
    {
        private IServiceCollection _services;
        private ServiceProvider _provider;
        private IServiceScope _scope;

        public SolidHttpCoreBuilder()
            : this(new ServiceCollection())
        {
        }
        public SolidHttpCoreBuilder(IServiceCollection services)
        {
            _services = services;

            var events = new SolidHttpEvents();
            services.AddSingleton<ISolidHttpEvents>(events);
            services.AddSingleton<ISolidHttpEventHandlerProvider>(events);

            services.AddSingleton<ISolidHttpInitializer, SolidHttpInitializer>();

            services.AddSingleton<IHttpClientCache, HttpClientCache>();
            services.AddTransient<IHttpClientFactory, TFactory>();

            services.AddScoped<ISolidHttpEventInvoker, SolidHttpEventInvoker>();
            services.AddScoped<ISolidHttpClientFactory, SolidHttpClientFactory>();

            services.AddSingleton<ISolidHttpOptions, SolidHttpOptions>();
        }

        public IServiceCollection Services => _services;

        public ISolidHttpCoreBuilder AddSolidHttpCoreOptions(Action<ISolidHttpOptions> configure)
        {
            _services.AddSingleton(configure);
            return this;
        }
        
        public ISolidHttpClientFactory Build()
        {
            if (_provider == null)
                _provider = _services.BuildServiceProvider();

            var initializer = _provider.GetRequiredService<ISolidHttpInitializer>();
            initializer.Initialize();

            if (_scope != null)
                _scope.Dispose();
            _scope = _provider.CreateScope();
            return _scope.ServiceProvider.GetRequiredService<ISolidHttpClientFactory>();
        }

        public void Dispose()
        {
            if (_provider != null)
                _provider.Dispose();

            if (_scope != null)
                _scope.Dispose();
        }
    }
}
