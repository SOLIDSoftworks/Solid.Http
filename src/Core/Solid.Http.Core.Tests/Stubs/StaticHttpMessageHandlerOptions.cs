using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Solid.Http.Core.Tests.Stubs
{
    public class StaticHttpMessageHandlerOptions
    {
        const string _defaultContentType = "application/json";
        const string _defaultCharType = "utf-8";

        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;
        public string ContentType { get; set; } = _defaultContentType;
        public string CharSet { get; set; } = _defaultCharType;
        public byte[] Content { get; set; }
        public void SetStringContent(string value, string contentType = _defaultContentType, string charSet = _defaultCharType)
        {
            if (!string.IsNullOrWhiteSpace(contentType))
                ContentType = contentType;
            if (!string.IsNullOrWhiteSpace(charSet))
                CharSet = charSet;
            if (!string.IsNullOrWhiteSpace(value))
                Content = Encoding.UTF8.GetBytes(value);
        }
    }
}
