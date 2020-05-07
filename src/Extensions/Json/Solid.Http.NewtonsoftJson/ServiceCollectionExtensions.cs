using Microsoft.Extensions.DependencyInjection.Extensions;
using Solid.Http;
using Solid.Http.NewtonsoftJson;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class Solid_Http_Json_ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureSolidHttpNewtonsoftJson(this IServiceCollection services, Action<SolidHttpNewtonsoftJsonOptions> configureOptions)
        {
            return services.Configure(configureOptions);
        }

    }
}
