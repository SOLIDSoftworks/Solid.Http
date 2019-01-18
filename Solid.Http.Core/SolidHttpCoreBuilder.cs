using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Solid.Http.Factories;
using System;
using System.Collections.Generic;
using System.Text;
using Solid.Http.Providers;
using Solid.Http.Abstractions;

namespace Solid.Http
{
    internal class SolidHttpCoreBuilder : ISolidHttpCoreBuilder
    {
        public IServiceCollection Services { get; set; }
        public IDictionary<string, object> Properties { get; set; }
    }
}
