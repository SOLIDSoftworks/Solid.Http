using Microsoft.Extensions.DependencyInjection.Extensions;
using Solid.Http;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class Solid_Http_Core_ServiceCollectionExtensions
    {
        public static IServiceCollection AddSolidHttpCore(this IServiceCollection services)
        {
            services.AddHttpClient();
            services.AddLogging();
            services.TryAddSingleton<IHttpClientProvider, HttpClientProvider>();
            services.TryAddSingleton<DeserializerProvider>();
            services.TryAddSingleton<ISolidHttpClientFactory, SolidHttpClientFactory>();
            services.TryAddTransient<SolidHttpClient>();
            services.TryAddTransient<SolidHttpRequest>();
            return services;
        }
        public static IServiceCollection AddSolidHttpCore(this IServiceCollection services, Action<SolidHttpBuilder> buildAction)
        {
            services.ConfigureSolidHttp(buildAction);
            return services.AddSolidHttpCore();
        }

        public static IServiceCollection ConfigureSolidHttp(this IServiceCollection services, Action<SolidHttpBuilder> buildAction)
        {
            var builder = new SolidHttpBuilder(services);
            buildAction(builder);
            return services;
        }

    }
}
