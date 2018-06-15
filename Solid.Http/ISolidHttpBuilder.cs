using Microsoft.Extensions.DependencyInjection;
using Solid.Http.Abstractions;
using System;

namespace Solid.Http
{
    public interface ISolidHttpBuilder : ISolidHttpOptionsBuilder<ISolidHttpBuilder>
    {
        IServiceCollection Services { get; }
    }
}