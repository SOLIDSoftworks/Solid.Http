using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;

namespace Solid.Http.Abstractions
{
    public interface ISolidHttpBuilder
    {
        IServiceCollection Services { get; }
        IDictionary<string, object> Properties { get; }
    }
}