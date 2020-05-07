using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;

namespace Solid.Http.NewtonsoftJson
{
    public class SolidHttpNewtonsoftJsonOptions
    {
        public JsonSerializerSettings SerializerSettings { get; set; } = SolidHttpNewtonsoftJsonOptionsDefaults.SerializerOptions;
        public List<MediaTypeHeaderValue> SupportedMediaTypes = SolidHttpNewtonsoftJsonOptionsDefaults.SupportedMediaTypes;
    }
}
