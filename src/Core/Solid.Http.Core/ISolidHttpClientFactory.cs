using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Solid.Http
{
    public interface ISolidHttpClientFactory
    {
        ValueTask<ISolidHttpClient> CreateAsync();
        ValueTask<ISolidHttpClient> CreateWithBaseAddressAsync(Uri baseAddress);
    }
}
