using System;
using Microsoft.Extensions.DependencyInjection;

namespace FluentHttp
{
    /// <summary>
    /// Extensions method for the service collection
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add FluentHttp to the service collection
        /// </summary>
        /// <typeparam name="TFactory">The custom FluentHttpClientFactory type</typeparam>
        /// <param name="services">The service collection</param>
        /// <returns>IFluentHttpSetup</returns>
        public static IFluentHttpSetup AddFluentHttp<TFactory>(this IServiceCollection services)
			where TFactory : FluentHttpClientFactory
		{
            services.AddSingleton<ISerializerProvider>(SerializerProvider.Instance);
            services.AddScoped<IFluentHttpClientFactoryEventInvoker, FluentHttpClientFactoryEvents>();
            services.AddScoped<IFluentHttpClientFactory, TFactory>();

            services.AddTransient<IFluentHttpOptions, FluentHttpOptions>();
            services.AddTransient<IFluentHttpSetup, FluentHttpSetup>();

            var provider = services.BuildServiceProvider();
            return provider.GetService<IFluentHttpSetup>();
		}

        /// <summary>
        /// Add FluentHttp to the service collection using the default implementation of FluentHttpClientFactory
        /// </summary>
        /// <param name="services">The service collection</param>
        /// <returns>IFluentHttpSetup</returns>
        public static IFluentHttpSetup AddFluentHttp(this IServiceCollection services)
		{
			return services.AddFluentHttp<FluentHttpClientFactory>();
		}
    }
}
