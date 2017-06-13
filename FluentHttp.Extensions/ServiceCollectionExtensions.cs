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
        public static IServiceCollection AddFluentHttp(this IServiceCollection services, FluentHttpOptions options = null)
        {
            services.AddSingleton<IHttpClientCache, HttpClientCache>();
            services.AddScoped<IFluentHttpClientFactory>(p =>
            {
                var factory = new FluentHttpClientFactory(p.GetRequiredService<IHttpClientCache>(), options?.Configuration);
                if (options != null && options.OnClientCreated != null)
                    factory.ClientCreated += (s, a) => options.OnClientCreated(s, a);
                if (options != null && options.OnRequestCreated != null)
                    factory.ClientCreated += (s1, a1) =>
                    {
                        a1.Client.RequestCreated += (s2, a2) =>
                        {
                            options.OnRequestCreated(s2, a2);
                        };
                    };

                return factory;
            });
            return services;
        }
    }
}
