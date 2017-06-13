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
        }
    }
}
