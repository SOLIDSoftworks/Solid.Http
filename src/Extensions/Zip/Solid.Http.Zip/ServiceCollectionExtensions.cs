using Microsoft.Extensions.DependencyInjection.Extensions;
using Solid.Http;
using Solid.Http.Zip;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class Solid_Http_Json_ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureSolidHttpZip(this IServiceCollection services, Action<SolidHttpZipOptions> configureOptions)
        {
            return services.Configure(configureOptions);
        }

    }
}
