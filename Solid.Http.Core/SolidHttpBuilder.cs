using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Solid.Http.Factories;
using System;
using System.Collections.Generic;
using System.Text;
using Solid.Http.Providers;
using Solid.Http.Abstractions;

namespace Solid.Http
{
    internal class SolidHttpBuilder : ISolidHttpCoreBuilder
    {
        public IServiceCollection Services { get; set; }
        public IDictionary<string, object> Properties { get; set; }
    }

    //    public class SolidHttpCoreBuilder : SolidHttpBuilderBase, ISolidHttpCoreBuilder, IDisposable
    //    {
    //        public SolidHttpCoreBuilder()
    //            : this(new ServiceCollection())
    //        {
    //        }
    //        public SolidHttpCoreBuilder(IServiceCollection services)
    //            : base(services)
    //        {
    //            var events = new SolidHttpEvents();

    //            Services.TryAddTransient<ISolidHttpClient, SolidHttpClient>();

    //            Services.TryAddSingleton<ISolidHttpEvents>(events);

    //            Services.TryAddSingleton<IHttpClientProvider, HttpClientProvider>();

    //            //Services.TryAddSingleton<IHttpClientCache, HttpClientCache>();
    //            //Services.TryAddTransient<IHttpClientFactory, TFactory>();

    //            Services.TryAddScoped<ISolidHttpClientFactory, SolidHttpClientFactory>();

    //            Services.TryAddSingleton<ISolidHttpOptions, SolidHttpOptions>();
    //        }

    //        public ISolidHttpCoreBuilder AddSolidHttpOptions(Action<ISolidHttpOptions> configure)
    //        {
    //            Services.AddSingleton(configure);
    //            return this;
    //        }
    //    }
}
