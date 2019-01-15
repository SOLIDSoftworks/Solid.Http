using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Solid.Http.Abstractions
{
    /// <summary>
    /// Deserializer
    /// </summary>
    public interface IDeserializer
    {
        /// <summary>
        /// Checks whether this deserializer can deserialize the specified mime type
        /// </summary>
        /// <param name="mimeType">A mime type</param>
        /// <returns>true or false</returns>
        bool CanDeserialize(string mimeType);

        /// <summary>
        /// Deserializes the HttpContext to an object
        /// </summary>
        /// <typeparam name="T">The object type</typeparam>
        /// <param name="content">The http content from the HttpResponseMessage</param>
        /// <returns>An awaitable task</returns>
        Task<T> DeserializeAsync<T>(HttpContent content);
    }
}
