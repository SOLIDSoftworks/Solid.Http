using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Solid.Http
{
    /// <summary>
    /// Extension methods for creating <see cref="ISolidHttpClient" />.
    /// </summary>
    public static class SolidHttpClientFactoryExtensions
    {
        /// <summary>
        /// Creates an <see cref="ISolidHttpClient" /> with a base address.
        /// </summary>
        /// <param name="factory">The <see cref="ISolidHttpClientFactory" /> being extended.</param>
        /// <param name="baseAddress">The base address to attach to the <see cref="ISolidHttpClient" />.</param>
        /// <returns>An <see cref="ISolidHttpClient" /> with a base address.</returns>
        public static ISolidHttpClient CreateWithBaseAddress(this ISolidHttpClientFactory factory, string baseAddress)
        {
            var url = new Uri(baseAddress, UriKind.Absolute);
            return factory.CreateWithBaseAddress(url);
        }
    }
}
