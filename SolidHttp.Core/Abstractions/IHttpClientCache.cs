using System;
using System.Net.Http;

namespace SolidHttp
{
    internal interface IHttpClientCache
    {
        HttpClient Get();
    }
}
