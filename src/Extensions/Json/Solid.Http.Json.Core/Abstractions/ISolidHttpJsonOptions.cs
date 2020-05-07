using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;

namespace Solid.Http.Json.Core.Abstractions
{
    public interface ISolidHttpJsonOptions
    {
        List<MediaTypeHeaderValue> SupportedMediaTypes { get; }
    }
}
