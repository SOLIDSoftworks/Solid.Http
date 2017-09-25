using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace FluentHttp
{
    public interface IFluentHttpSerializer
    {
        Func<HttpContent, Task<T>> CreateDeserializer<T>();
        //Func<T, string> CreateSerializer<T>();
    }
}
