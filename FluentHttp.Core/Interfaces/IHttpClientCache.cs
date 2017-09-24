using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace FluentHttp
{
    public interface IHttpClientCache
    {
        bool Exists(string key);
        HttpClient Get(string key);
        HttpClient Cache(string key, HttpClient client);
    }
}
