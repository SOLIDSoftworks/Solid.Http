using System;
namespace FluentHttp
{
    /// <summary>
    /// The IFluentHttpOptions interface
    /// </summary>
    public interface IFluentHttpOptions
    {
        /// <summary>
        /// The serializer provider for FluentHttp
        /// </summary>
        ISerializerProvider Serializers { get; }

        /// <summary>
        /// The events to be triggered when a FluentHttpClient is created
        /// </summary>
        IFluentHttpClientFactoryEvents FactoryEvents { get; }
    }
}
