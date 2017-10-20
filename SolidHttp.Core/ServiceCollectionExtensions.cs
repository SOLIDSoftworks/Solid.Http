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
        /// <typeparam name="TFactory">The custom IHttpClientFactory type</typeparam>
        /// <param name="services">The service collection</param>
        /// <returns>ISolidHttpSetup</returns>
        public static ISolidHttpSetup AddSolidHttp<TFactory>(this IServiceCollection services)
            where TFactory : class, IHttpClientFactory
        {
            services.AddSingleton<IDeserializerProvider>(DeserializerProvider.Instance);
            services.AddSingleton<ISolidHttpEventInvoker>(new SolidHttpEvents());
            services.AddSingleton<IHttpClientCache, HttpClientCache>();
            services.AddTransient<IHttpClientFactory, TFactory>();
            services.AddScoped<ISolidHttpClientFactory, SolidHttpClientFactory>();

            services.AddSingleton<ISolidHttpOptions, SolidHttpOptions>();
            services.AddSingleton<ISolidHttpSetup, SolidHttpSetup>();

            var provider = services.BuildServiceProvider();
            return provider.GetService<ISolidHttpSetup>();
        }

        /// <summary>
        /// Add SolidHttp to the service collection using the default implementation of IHttpClientFactory
        /// </summary>
        /// <param name="services">The service collection</param>
        /// <returns>ISolidHttpSetup</returns>
        public static ISolidHttpSetup AddSolidHttp(this IServiceCollection services)
        {
            return services.AddSolidHttp<SimpleHttpClientFactory>();
        }
    }
}
