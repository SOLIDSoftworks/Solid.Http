using System;
using System.Net.Http;

namespace Solid.Http.Abstractions
{
    internal interface IHttpClientCache
    {
        HttpClient Get();
    }
}
