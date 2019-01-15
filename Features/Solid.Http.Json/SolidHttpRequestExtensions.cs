using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Solid.Http.Abstractions
{
    internal static class SolidHttpRequestExtensions
    {
        public static JsonSerializerSettings GetJsonSerializerSettings(this ISolidHttpRequest request)
        {
            return request.BaseRequest.Properties["JsonSerializerSettings"] as JsonSerializerSettings;
        }
    }
}
