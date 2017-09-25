using System;
namespace FluentHttp
{
    public interface IFluentHttpOptions
    {
        ISerializerProvider Serializers { get; }
        IFluentHttpClientFactory Factory { get; }
    }
}
