using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FluentHttp
{
    public static class JsonExtensions
    {
        private static readonly JsonSerializerSettings DEFAULT_SERIALIZER_SETTINGS;
        static JsonExtensions()
        {
            DEFAULT_SERIALIZER_SETTINGS = new JsonSerializerSettings
            {

            };
        }
        public static async Task<T> AsJson<T>(this FluentHttpRequest request, JsonSerializerSettings settings = null)
        {
            var response = await request;
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(json, settings ?? DEFAULT_SERIALIZER_SETTINGS);
        }

        public static Task<T> AsJson<T>(this FluentHttpRequest request, T schema, JsonSerializerSettings settings = null)
        {
            return request.AsJson<T>(settings);
        }

        public async static Task<IEnumerable<T>> AsJsonArray<T>(this FluentHttpRequest request, JsonSerializerSettings settings = null)
        {
            return await request.AsJson<IEnumerable<T>>(settings);
        }

        public static Task<IEnumerable<T>> AsJsonArray<T>(this FluentHttpRequest request, T schema, JsonSerializerSettings settings = null)
        {
            return request.AsJson<IEnumerable<T>>(settings);
        }

        public static FluentHttpRequest WithJsonContent<T>(this FluentHttpRequest request, T body, JsonSerializerSettings settings = null)
        {
            var json = JsonConvert.SerializeObject(body, settings ?? DEFAULT_SERIALIZER_SETTINGS);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            return request.WithContent(content);
        }
    }
}
