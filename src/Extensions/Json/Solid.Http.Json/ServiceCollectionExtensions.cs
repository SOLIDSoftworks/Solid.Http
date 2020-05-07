using Microsoft.Extensions.DependencyInjection.Extensions;
using Solid.Http;
using Solid.Http.Json;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class Solid_Http_Json_ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureSolidHttpJson(this IServiceCollection services, Action<SolidHttpJsonOptions> configureOptions)
        {
            return services.Configure(configureOptions);
        }

    }
}
