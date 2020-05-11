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
    public abstract class JsonDeserializerBase<TOptions> : IJsonDeserializer, IDeserializer, IDisposable
        where TOptions : ISolidHttpJsonOptions
    {
        private IDisposable _optionsChangeToken;
        protected JsonDeserializerBase(IOptionsMonitor<TOptions> monitor)
        {
            Options = monitor.CurrentValue;
            _optionsChangeToken = monitor.OnChange((options, _) => Options = options);
        }

        public TOptions Options { get; private set; }

        public virtual bool CanDeserialize(string mediaType, Type typeToReturn) => Options.SupportedMediaTypes.Any(m => m.MediaType.Equals(mediaType, StringComparison.OrdinalIgnoreCase));
        public abstract ValueTask<T> DeserializeAsync<T>(HttpContent content);

        public virtual void Dispose()
        {
            _optionsChangeToken?.Dispose();
        }
    }
}
