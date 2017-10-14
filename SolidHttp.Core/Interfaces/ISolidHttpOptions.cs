using System;
namespace SolidHttp
{
    /// <summary>
    /// The ISolidHttpOptions interface
    /// </summary>
    public interface ISolidHttpOptions
    {
        /// <summary>
        /// The deserializer provider for SolidHttp
        /// </summary>
        IDeserializerProvider Deserializers { get; }

        /// <summary>
        /// The events to be triggered when a SolidHttpClient is created
        /// </summary>
        ISolidHttpEvents Events { get; }
    }
}
