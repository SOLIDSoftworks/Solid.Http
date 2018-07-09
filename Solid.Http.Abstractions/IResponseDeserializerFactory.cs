using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Solid.Http
{
    /// <summary>
    /// The ISolidHttpSerializer interface
    /// </summary>
    public interface IResponseDeserializerFactory
    {
        /// <summary>
        /// Creates a deserializer
        /// </summary>
        /// <typeparam name="T">The type to deserialize to</typeparam>
        /// <returns>A deserializer which takes in HttpContent and returns a Task of type T</returns>
        Func<HttpContent, Task<T>> CreateDeserializer<T>();
    }
}
