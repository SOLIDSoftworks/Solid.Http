using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Solid.Http
{
    /// <summary>
    /// Extension methods for registering handlers on <see cref="ISolidHttpClient" />.
    /// </summary>
    public static class Solid_Http_SolidHttpClientExtensions_Handlers
    {
        /// <summary>
        /// Registers an event handler that runs when a new <see cref="ISolidHttpRequest" /> is created.
        /// </summary>
        /// <param name="client">The <see cref="ISolidHttpClient" /> being extended.</param>
        /// <param name="handler">The event handler that runs when a new <see cref="ISolidHttpRequest" /> is created.</param>
        /// <returns>The <see cref="ISolidHttpClient" /> so the additional calls can be chained.</returns>
        public static ISolidHttpClient OnRequestCreated(this ISolidHttpClient client, Action<ISolidHttpRequest> handler)
            => client.OnRequestCreated(handler.Convert());
    }
}
