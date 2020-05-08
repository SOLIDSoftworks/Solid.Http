using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Solid.Http
{
    public interface IHttpClientProvider
    {
        HttpClient Get(Uri url);
    }
}
