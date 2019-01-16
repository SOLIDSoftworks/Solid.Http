using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;

namespace Solid.Http.Abstractions
{
    /// <summary>
    /// A builder used to configure Solid.Http.Builder
    /// </summary>
    public interface ISolidHttpBuilder
    {
        /// <summary>
        /// The service collection
        /// </summary>
        IServiceCollection Services { get; }
        /// <summary>
        /// Properties that can be used in extensions
        /// </summary>
        IDictionary<string, object> Properties { get; }
    }
}