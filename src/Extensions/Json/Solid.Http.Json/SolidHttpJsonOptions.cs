using Solid.Http.Json.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Solid.Http.Json
{
    public class SolidHttpJsonOptions : ISolidHttpJsonOptions
    {
        public JsonSerializerOptions SerializerOptions { get; set; } = SolidHttpJsonOptionsDefaults.SerializerOptions;
        public List<MediaTypeHeaderValue> SupportedMediaTypes => SolidHttpJsonOptionsDefaults.SupportedMediaTypes;
    }
}
