using System;
using System.Collections.Generic;
using System.Text;

namespace FluentHttp
{
    public static class HeaderExtensions
    {
        public static FluentHttpRequest WithHeader(this FluentHttpRequest request, string name, string value)
        {
            request.BaseRequest.Headers.Add(name, value);
            return request;
        }

        public static FluentHttpRequest WithContentHeader(this FluentHttpRequest request, string name, string value)
        {
            if (request.BaseRequest.Content == null)
                throw new InvalidOperationException("Cannot set a content header on null content");
            request.BaseRequest.Content.Headers.Add(name, value);
            return request;
        }
    }
}
