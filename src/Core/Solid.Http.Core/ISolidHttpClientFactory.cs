using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Solid.Http
{
    /// <summary>
    /// An interface that describes a factory that can create <see cref="ISolidHttpClient" />.
    /// </summary>
    public interface ISolidHttpClientFactory
    {
        /// <summary>
        /// Creates an <see cref="ISolidHttpClient" />.
        /// </summary>
        /// <returns>An <see cref="ISolidHttpClient" />.</returns>
        ISolidHttpClient Create();

        /// <summary>
        /// Creates an <see cref="ISolidHttpClient" /> with a base address.
        /// </summary>
        /// <param name="baseAddress">The base address to attach to the <see cref="ISolidHttpClient" />.</param>
        /// <returns>An <see cref="ISolidHttpClient" /> with a base address.</returns>
        ISolidHttpClient CreateWithBaseAddress(Uri baseAddress);
    }
}
