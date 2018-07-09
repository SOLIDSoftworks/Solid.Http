using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Solid.Http
{
    public interface IDeserializer
    {
        bool CanDeserialize(string mimeType);

        Task<T> DeserializeAsync<T>(HttpContent content);
    }
}
