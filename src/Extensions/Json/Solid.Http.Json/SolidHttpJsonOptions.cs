using Solid.Http.Json.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Solid.Http.Json
{
    /// <summary>
    /// Options for configuring Solid.Http.Json.
    /// </summary>
    public class SolidHttpJsonOptions : ISolidHttpJsonOptions
    {
        /// <summary>
        /// The <see cref="JsonSerializerOptions" /> used for deserializing JSON.
        /// </summary>
        public JsonSerializerOptions SerializerOptions { get; set; } = SolidHttpJsonOptionsDefaults.SerializerOptions;

        /// <summary>
        /// The supported JSON media types.
        /// </summary>
        public List<MediaTypeHeaderValue> SupportedMediaTypes => SolidHttpJsonOptionsDefaults.SupportedMediaTypes;
    }
}
