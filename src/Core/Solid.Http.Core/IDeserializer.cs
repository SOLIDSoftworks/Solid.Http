using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Solid.Http
{
    /// <summary>
    /// An interface that describes a deserializer.
    /// </summary>
    public interface IDeserializer
    {
        /// <summary>
        /// Checks whether this deserializer can deserialize the <paramref name="mediaType" /> into a the <paramref name="typeToReturn" />.
        /// </summary>
        /// <param name="mediaType">A mime type.</param>
        /// <param name="typeToReturn">A <see cref="Type" /> to deserialize to.</param>
        /// <returns>true or false</returns>
        bool CanDeserialize(string mediaType, Type typeToReturn);

        /// <summary>
        /// Deserializes the <see cref="HttpContent" /> to an instance of <typeparamref name="T" />.
        /// </summary>
        /// <typeparam name="T">The type to deserialize to.</typeparam>
        /// <param name="content">The <see cref="HttpContent" /> from the <seealso cref="HttpResponseMessage" /></param>
        /// <returns>A <see cref="ValueTask{T}" /> of <typeparamref name="T" />.</returns>
        ValueTask<T> DeserializeAsync<T>(HttpContent content);
    }
}
