using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
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

    public class SolidHttpCoreBuilder<TFactory> : SolidHttpBuilderBase, ISolidHttpCoreBuilder, IDisposable
        where TFactory : class, IHttpClientFactory
    {
        public SolidHttpCoreBuilder()
            : this(new ServiceCollection())
        {
        }
        public SolidHttpCoreBuilder(IServiceCollection services)
            : base(services)
        {
            var events = new SolidHttpEvents();
            Services.TryAddSingleton<ISolidHttpEvents>(events);
            Services.TryAddSingleton<ISolidHttpEventHandlerProvider>(events);

            Services.TryAddSingleton<IHttpClientCache, HttpClientCache>();
            Services.TryAddTransient<IHttpClientFactory, TFactory>();

            Services.TryAddScoped<ISolidHttpEventInvoker, SolidHttpEventInvoker>();
            Services.TryAddScoped<ISolidHttpClientFactory, SolidHttpClientFactory>();

            Services.TryAddSingleton<ISolidHttpOptions, SolidHttpOptions>();
        }

        public SolidHttpCoreBuilder<TFactory> AddSolidHttpCoreOptions(Action<ISolidHttpOptions> configure)
        {
            Services.AddSingleton(configure);
            return this;
        }

        ISolidHttpCoreBuilder ISolidHttpCoreBuilder.AddSolidHttpCoreOptions(Action<ISolidHttpOptions> configure)
        {
            return AddSolidHttpCoreOptions(configure);
        }
    }
}
