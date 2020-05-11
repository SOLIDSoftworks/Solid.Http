using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;

namespace Solid.Http.Json.Core.Abstractions
{
    /// <summary>
    /// An interface that represents a deserializer for JSON <see cref="HttpContent" />.
    /// </summary>
    public interface IJsonDeserializer : IDeserializer
    {
    }
}
