using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace FluentHttp
{
    public interface ISerializerProvider
    {
        Func<HttpContent, Task<T>> GetSerializer<T>(string mimeType);
        void AddSerializer(IFluentHttpSerializer serializer, string mimeType, params string[] more);
    }
}
