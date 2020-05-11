using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Solid.Http
{
    /// <summary>
    /// An interface that describes a provider that returns an <see cref="HttpClient" /> for a specified <see cref="Uri" />.
    /// </summary>
    public interface IHttpClientProvider
    {
        /// <summary>
        /// Gets an <see cref="HttpClient" /> for a specified <see cref="Uri" />.
        /// </summary>
        /// <param name="url">The <see cref="Uri" /> that will be requested.</param>
        /// <returns>An <see cref="HttpClient" />.</returns>
        HttpClient Get(Uri url);
    }
}
