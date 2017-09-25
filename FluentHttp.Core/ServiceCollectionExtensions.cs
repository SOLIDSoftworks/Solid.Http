using System;
using Microsoft.Extensions.DependencyInjection;

namespace FluentHttp
{
    public static class ServiceCollectionExtensions
    {
        public static IFluentHttpOptions AddFluentHttp<TFactory>(this IServiceCollection services)
			where TFactory : FluentHttpClientFactory
		{
			services.AddSingleton<IHttpClientCache, HttpClientCache>();
            services.AddSingleton<ISerializerProvider>(SerializerProvider.Instance);
            services.AddSingleton<IFluentHttpClientFactory, TFactory>();
            services.AddTransient<IFluentHttpOptions, FluentHttpOptions>();

            var provider = services.BuildServiceProvider();
            return provider.GetService<IFluentHttpOptions>();
		}

        public static IFluentHttpOptions AddFluentHttp(this IServiceCollection services)
		{
			return services.AddFluentHttp<FluentHttpClientFactory>();
		}
    }
}
