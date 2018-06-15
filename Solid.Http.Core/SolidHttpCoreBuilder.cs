using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Solid.Http.Abstractions;
using Solid.Http.Events;
using Solid.Http.Factories;
using Solid.Http.Setup;
using System;
using System.Collections.Generic;
using System.Text;
using Solid.Http.Providers;

namespace Solid.Http
{
    public class SolidHttpCoreBuilder : SolidHttpBuilderBase, ISolidHttpCoreBuilder, IDisposable
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

            Services.TryAddSingleton<IHttpClientProvider, HttpClientProvider>();

            //Services.TryAddSingleton<IHttpClientCache, HttpClientCache>();
            //Services.TryAddTransient<IHttpClientFactory, TFactory>();

            Services.TryAddScoped<ISolidHttpEventInvoker, SolidHttpEventInvoker>();
            Services.TryAddScoped<ISolidHttpClientFactory, SolidHttpClientFactory>();

            Services.TryAddSingleton<ISolidHttpOptions, SolidHttpOptions>();
        }

        public ISolidHttpCoreBuilder AddSolidHttpOptions(Action<ISolidHttpOptions> configure)
        {
            Services.AddSingleton(configure);
            return this;
        }
    }
}
