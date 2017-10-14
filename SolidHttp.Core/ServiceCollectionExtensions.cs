using System;
using Microsoft.Extensions.DependencyInjection;

namespace SolidHttp
{
    /// <summary>
    /// Extensions method for the service collection
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add SolidHttp to the service collection
        /// </summary>
        /// <typeparam name="TFactory">The custom SolidHttpClientFactory type</typeparam>
        /// <param name="services">The service collection</param>
        /// <returns>ISolidHttpSetup</returns>
        public static ISolidHttpSetup AddSolidHttp<TFactory>(this IServiceCollection services)
            where TFactory : SolidHttpClientFactory
        {
            services.AddSingleton<IDeserializerProvider>(DeserializerProvider.Instance);
            services.AddSingleton<ISolidHttpEventInvoker, SolidHttpEvents>();
            services.AddScoped<ISolidHttpClientFactory, TFactory>();

            services.AddTransient<ISolidHttpOptions, SolidHttpOptions>();
            services.AddTransient<ISolidHttpSetup, SolidHttpSetup>();

            var provider = services.BuildServiceProvider();
            return provider.GetService<ISolidHttpSetup>();
        }

        /// <summary>
        /// Add SolidHttp to the service collection using the default implementation of SolidHttpClientFactory
        /// </summary>
        /// <param name="services">The service collection</param>
        /// <returns>ISolidHttpSetup</returns>
        public static ISolidHttpSetup AddSolidHttp(this IServiceCollection services)
        {
            return services.AddSolidHttp<SolidHttpClientFactory>();
        }
    }
}
