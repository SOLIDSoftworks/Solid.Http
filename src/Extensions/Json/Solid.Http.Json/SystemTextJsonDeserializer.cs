using Microsoft.Extensions.Options;
using Solid.Http.Json.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Solid.Http.Json
{
    internal class SystemTextJsonDeserializer : JsonDeserializerBase<SolidHttpJsonOptions>
    {

        public SystemTextJsonDeserializer(IOptionsMonitor<SolidHttpJsonOptions> monitor)
            : base(monitor)
        {
        }

        public override async ValueTask<T> DeserializeAsync<T>(HttpContent content)
        {
            using (var stream = await content.ReadAsStreamAsync())
            {
                return await JsonSerializer.DeserializeAsync<T>(stream, Options.SerializerOptions);
            }
        }
    }
}
