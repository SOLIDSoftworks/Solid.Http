using Solid.Http.Models;
using System;
namespace Solid.Http
{
    /// <summary>
    /// The ISolidHttpOptions interface
    /// </summary>
    public interface ISolidHttpOptions
    {
        /// <summary>
        /// The global events to be triggered during SolidHttp events
        /// </summary>
        ISolidHttpEvents Events { get; }
        HttpClientStrategy Strategy { get; set; }
    }
}
