using Microsoft.Extensions.DependencyInjection.Extensions;
using Solid.Http;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class Solid_Http_Core_ServiceCollectionExtensions
    {
        public static IServiceCollection AddSolidHttp(this IServiceCollection services) =>
            services.AddSolidHttpCore(builder => builder.AddJson());

        public static IServiceCollection AddSolidHttp(this IServiceCollection services, Action<SolidHttpBuilder> buildAction)
            => services.AddSolidHttp().ConfigureSolidHttp(buildAction);
    }
}
