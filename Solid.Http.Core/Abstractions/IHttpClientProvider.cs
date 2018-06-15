using System;
using System.Net.Http;

namespace Solid.Http.Abstractions
{
    public interface IHttpClientProvider
    {
        HttpClient Get();
    }
}
