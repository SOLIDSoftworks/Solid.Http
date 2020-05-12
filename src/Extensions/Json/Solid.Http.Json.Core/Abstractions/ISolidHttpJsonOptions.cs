using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;

namespace Solid.Http.Json.Core.Abstractions
{
    /// <summary>
    /// An interface that represents options for JSON deserializers.
    /// </summary>
    public interface ISolidHttpJsonOptions
    {
        /// <summary>
        /// The supported JSON media types.
        /// </summary>
        List<MediaTypeHeaderValue> SupportedMediaTypes { get; }
    }
}
