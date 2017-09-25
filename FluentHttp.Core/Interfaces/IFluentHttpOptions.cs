using System;
namespace FluentHttp
{
    public interface IFluentHttpOptions
    {
        ISerializerProvider Serializers { get; }
        IFluentHttpClientFactoryEvents FactoryEvents { get; }
    }
}
