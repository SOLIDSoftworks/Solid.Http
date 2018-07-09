using System;
using System.Net.Http;

namespace Solid.Http
{
    public interface IHttpClientProvider
    {
        HttpClient Get(Uri url);
    }
}
