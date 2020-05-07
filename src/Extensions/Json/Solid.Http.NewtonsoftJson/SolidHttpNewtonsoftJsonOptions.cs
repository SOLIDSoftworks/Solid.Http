using Newtonsoft.Json;
using Solid.Http.Json.Core;
using Solid.Http.Json.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;

namespace Solid.Http.NewtonsoftJson
{
    public class SolidHttpNewtonsoftJsonOptions : ISolidHttpJsonOptions
    {
        public JsonSerializerSettings SerializerSettings { get; set; } = SolidHttpNewtonsoftJsonOptionsDefaults.SerializerOptions;
        public List<MediaTypeHeaderValue> SupportedMediaTypes => SolidHttpNewtonsoftJsonOptionsDefaults.SupportedMediaTypes;
    }
}
