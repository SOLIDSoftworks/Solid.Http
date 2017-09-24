using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace FluentHttp
{
    public static class ResponseExtensions
    {
		public static async Task<T> As<T>(this FluentHttpRequest request, Func<HttpContent, Task<T>> deserialize)
		{
            var content = await request.GetContentAsync();
            if (content == null) return default(T); // should we maybe throw an exception if there is no content?
			return await deserialize(content);
		}

        public static async Task<T> As<T>(this FluentHttpRequest request, T anonymous)
        {
            return await request.As<T>();
        }

        public static async Task<T> As<T>(this FluentHttpRequest request)
		{
			var content = await request.GetContentAsync();
			if (content == null) return default(T); // should we maybe throw an exception if there is no content?

            var mime = content?.Headers?.ContentType?.MediaType;

			var serializer = request.Client.Serializers.GetSerializer<T>(mime);
            if (serializer == null)
                throw new InvalidOperationException($"Cannot deserialize {mime} response as {typeof(T).FullName}");
			return await serializer(content);
		}

        public static async Task<string> AsText(this FluentHttpRequest request)
		{
            return await request.As(async content => await content.ReadAsStringAsync());
		}

        private static async Task<HttpContent> GetContentAsync(this FluentHttpRequest request)
        {
            var response = await request;
            return response.Content;
        }
    }
}
