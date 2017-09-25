using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace FluentHttp
{
    public interface IHttpClientProvider
    {
        HttpClient Get();
    }
}
