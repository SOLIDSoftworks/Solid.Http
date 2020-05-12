using Microsoft.Extensions.Options;
using Solid.Http.Json.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Solid.Http.Json.Core
{
    /// <summary>
    /// A base deserializer for JSON <see cref="HttpContent" />.
    /// </summary>
    /// <typeparam name="TOptions">The type that contains options.</typeparam>
    public abstract class JsonDeserializerBase<TOptions> : IJsonDeserializer, IDeserializer, IDisposable
        where TOptions : ISolidHttpJsonOptions
    {
        private IDisposable _optionsChangeToken;
        /// <summary>
        /// Creates a new <see cref="JsonDeserializerBase{TOptions}" />.
        /// </summary>
        /// <param name="monitor">The options monitor for options of type <typeparamref name="TOptions" />.</param>
        protected JsonDeserializerBase(IOptionsMonitor<TOptions> monitor)
        {
            Options = monitor.CurrentValue;
            _optionsChangeToken = monitor.OnChange((options, _) => Options = options);
        }

        /// <summary>
        /// The current instance of <typeparamref name="TOptions" />.
        /// </summary>
        public TOptions Options { get; private set; }

        /// <summary>
        /// Checks whether this deserializer can deserialize the <paramref name="mediaType" /> into a the <paramref name="typeToReturn" />.
        /// </summary>
        /// <param name="mediaType">A mime type.</param>
        /// <param name="typeToReturn">A <see cref="Type" /> to deserialize to.</param>
        /// <returns>true or false</returns>
        public virtual bool CanDeserialize(string mediaType, Type typeToReturn) => Options.SupportedMediaTypes.Any(m => m.MediaType.Equals(mediaType, StringComparison.OrdinalIgnoreCase));

        /// <summary>
        /// Deserializes the <see cref="HttpContent" /> to an instance of <typeparamref name="T" />.
        /// </summary>
        /// <typeparam name="T">The type to deserialize to.</typeparam>
        /// <param name="content">The <see cref="HttpContent" /> from the <seealso cref="HttpResponseMessage" /></param>
        /// <returns>A <see cref="ValueTask{T}" /> of <typeparamref name="T" />.</returns>
        public abstract ValueTask<T> DeserializeAsync<T>(HttpContent content);

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public virtual void Dispose()
        {
            _optionsChangeToken?.Dispose();
        }
    }
}
