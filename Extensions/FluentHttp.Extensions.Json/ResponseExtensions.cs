using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FluentHttp
{
    public static class ResponseExtensions
    {
        public static async Task<T> As<T>(this FluentHttpRequest request, JsonSerializerSettings settings)
        {
            var serializer = new FluentHttpJsonSerializer(settings);
            var deserialize = serializer.CreateDeserializer<T>();
            return await request.As<T>(deserialize);
        }
        public static Task<T> As<T>(this FluentHttpRequest request, T anonymous, JsonSerializerSettings settings)
        {
            return request.As<T>(settings);
        }
        public static Task<IEnumerable<T>> AsMany<T>(this FluentHttpRequest request, JsonSerializerSettings settings)
        {
            return request.As<IEnumerable<T>>(settings);
        }
        public static Task<IEnumerable<T>> AsMany<T>(this FluentHttpRequest request, T anonymous, JsonSerializerSettings settings)
        {
            return request.As<IEnumerable<T>>(settings);
        }
    }
}
