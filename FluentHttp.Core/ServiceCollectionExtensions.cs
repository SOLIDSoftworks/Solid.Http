using System;
using Microsoft.Extensions.DependencyInjection;

namespace FluentHttp
{
    public static class ServiceCollectionExtensions
    {
        public static IFluentHttpSetup AddFluentHttp<TFactory>(this IServiceCollection services)
			where TFactory : FluentHttpClientFactory
		{
			services.AddSingleton<IHttpClientProvider, HttpClientProvider>();
            services.AddSingleton<ISerializerProvider>(SerializerProvider.Instance);
            services.AddScoped<IFluentHttpClientFactoryEventInvoker, FluentHttpClientFactoryEvents>();
            services.AddScoped<IFluentHttpClientFactory, TFactory>();

            services.AddTransient<IFluentHttpOptions, FluentHttpOptions>();
            services.AddTransient<IFluentHttpSetup, FluentHttpSetup>();

            var provider = services.BuildServiceProvider();
            return provider.GetService<IFluentHttpSetup>();
		}

        public static IFluentHttpSetup AddFluentHttp(this IServiceCollection services)
		{
			return services.AddFluentHttp<FluentHttpClientFactory>();
		}
    }
}
