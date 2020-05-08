using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Solid.Http
{
    public interface ISolidHttpClientFactory
    {
        ISolidHttpClient Create();
        ISolidHttpClient CreateWithBaseAddress(Uri baseAddress);
    }
}
