using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Solid.Http
{
    /// <summary>
    /// The ISolidHttpBuilder interface
    /// </summary>
    public interface ISolidHttpCoreBuilder : ISolidHttpOptionsBuilder<ISolidHttpCoreBuilder>
    {
        IServiceCollection Services { get; }
    }
}
