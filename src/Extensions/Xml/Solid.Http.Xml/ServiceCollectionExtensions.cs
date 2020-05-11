using Microsoft.Extensions.DependencyInjection.Extensions;
using Solid.Http;
using Solid.Http.Xml;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class Solid_Http_Json_ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureSolidHttpXml(this IServiceCollection services, Action<SolidHttpXmlOptions> configureOptions)
        {
            return services.Configure(configureOptions);
        }
    }
}
