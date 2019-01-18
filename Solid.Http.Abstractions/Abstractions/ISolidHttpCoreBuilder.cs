using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Solid.Http.Abstractions
{
    /// <summary>
    /// The ISolidHttpBuilder interface
    /// </summary>
    public interface ISolidHttpCoreBuilder
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
