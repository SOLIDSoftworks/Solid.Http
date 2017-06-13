using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FluentHttp
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddFluentHttp(this IServiceCollection services, IConfigurationRoot configuration = null)
        {
            services.AddSingleton<IHttpClientCache, HttpClientCache>();
            services.AddScoped<IFluentHttpClientFactory>(p =>
            {
                return new FluentHttpClientFactory(p.GetRequiredService<IHttpClientCache>(), configuration);
            });
            return services;
        }
    }
}
