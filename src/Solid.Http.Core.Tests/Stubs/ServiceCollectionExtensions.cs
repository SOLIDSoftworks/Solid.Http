using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Http;
using Solid.Http.Core.Tests.Stubs;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
    internal static class Solid_Http_Core_Tests_ServiceCollectionExtensions
    {
        public static IServiceCollection AddStaticHttpMessageHandler(this IServiceCollection services, Action<StaticHttpMessageHandlerOptions> configureOptions)
        {
            services.Configure(configureOptions);
            services.ConfigureAll<HttpClientFactoryOptions>(options => options.HttpMessageHandlerBuilderActions.Add(builder => builder.PrimaryHandler = builder.Services.GetService<StaticHttpMessageHandler>()));
            services.TryAddTransient<StaticHttpMessageHandler>();
            return services;
        }
    }
}
