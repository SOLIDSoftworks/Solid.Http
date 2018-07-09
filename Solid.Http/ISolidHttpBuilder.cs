using Microsoft.Extensions.DependencyInjection;

using System;

namespace Solid.Http
{
    public interface ISolidHttpBuilder : ISolidHttpOptionsBuilder<ISolidHttpBuilder>
    {
        IServiceCollection Services { get; }
    }
}