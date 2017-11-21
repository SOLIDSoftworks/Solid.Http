using System;
using System.Net.Http;

namespace SolidHttp.Abstractions
{
    internal interface IHttpClientCache
    {
        HttpClient Get();
    }
}
