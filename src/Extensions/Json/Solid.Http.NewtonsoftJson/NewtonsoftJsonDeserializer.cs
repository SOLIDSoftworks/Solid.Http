using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Solid.Http.Json.Core;
using Solid.Http.NewtonsoftJson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Solid.Http.Json
{
    internal class NewtonsoftJsonDeserializer : JsonDeserializerBase<SolidHttpNewtonsoftJsonOptions>
    {
        public NewtonsoftJsonDeserializer(IOptionsMonitor<SolidHttpNewtonsoftJsonOptions> monitor) : base(monitor)
        {
        }

        public override async ValueTask<T> DeserializeAsync<T>(HttpContent content)
        {
            var json = await content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(json, Options.SerializerSettings);           
        }
    }
}
