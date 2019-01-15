using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Solid.Http.Abstractions
{
    public interface ISolidHttpClient
    {
        IEnumerable<IDeserializer> Deserializers { get; }
        void AddProperty<T>(string key, T value);
        T GetProperty<T>(string key);
        ISolidHttpRequest PerformRequestAsync(HttpMethod method, Uri url, CancellationToken cancellationToken);
        ISolidHttpClient OnRequestCreated(Action<IServiceProvider, ISolidHttpRequest> handler);
    }
}
