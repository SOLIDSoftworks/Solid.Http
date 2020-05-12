using Newtonsoft.Json;
using Solid.Http.Json.Core;
using Solid.Http.Json.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;

namespace Solid.Http.NewtonsoftJson
{
    /// <summary>
    /// Options for configuring Solid.Http.NewtonsoftJson
    /// </summary>
    public class SolidHttpNewtonsoftJsonOptions : ISolidHttpJsonOptions
    {
        /// <summary>
        /// The <see cref="JsonSerializerSettings" /> used for deserializing JSON.
        /// </summary>
        public JsonSerializerSettings SerializerSettings { get; set; } = SolidHttpNewtonsoftJsonOptionsDefaults.SerializerOptions;

        /// <summary>
        /// The supported JSON media types.
        /// </summary>
        public List<MediaTypeHeaderValue> SupportedMediaTypes => SolidHttpNewtonsoftJsonOptionsDefaults.SupportedMediaTypes;
    }
}
