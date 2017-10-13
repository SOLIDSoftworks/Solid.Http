using System;
namespace FluentHttp
{
    /// <summary>
    /// The IFluentHttpOptions interface
    /// </summary>
    public interface IFluentHttpOptions
    {
        /// <summary>
        /// The deserializer provider for FluentHttp
        /// </summary>
        IDeserializerProvider Deserializers { get; }

        /// <summary>
        /// The events to be triggered when a FluentHttpClient is created
        /// </summary>
        IFluentHttpEvents Events { get; }
    }
}
