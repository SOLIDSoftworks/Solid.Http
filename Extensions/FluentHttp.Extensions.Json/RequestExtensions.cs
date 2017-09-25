using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FluentHttp
{
    public static class RequestExtensions
    {
        public static FluentHttpRequest WithJsonContent<T>(this FluentHttpRequest request, T body, JsonSerializerSettings settings = null)
        {
            var json = JsonConvert.SerializeObject(body, settings ?? DefaultSerializerSettingsProvider.DefaultSettings);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            return request.WithContent(content);
        }
    }
}
