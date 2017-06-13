using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FluentHttp
{
    public static class ResponseExtensions
    {
        public static FluentHttpRequest ExpectSuccess(this FluentHttpRequest request)
        {
            request.OnResponse += async (sender, args) =>
            {
                if (!args.Response.IsSuccessStatusCode)
                {
                    var message = await GenerateNonSuccessMessage(args.Response);
                    // TODO: reevaluate this exception type. maybe a seperate type for server error and client error
                    throw new InvalidOperationException(message);
                }
            };
            return request;
        }

        public static FluentHttpRequest On(this FluentHttpRequest request, HttpStatusCode code, Action<HttpResponseMessage> handler)
        {
            request.OnResponse += (sender, args) =>
            {
                if (args.Response.StatusCode == code)
                    handler(args.Response);
            };
            return request;
        }

        public static FluentHttpRequest On(this FluentHttpRequest request, HttpStatusCode code, Func<HttpResponseMessage, Task> handler)
        {
            request.OnResponse += async (sender, args) =>
            {
                if (args.Response.StatusCode == code)
                    await handler(args.Response);
            };
            return request;
        }
        //public async Task<T> As<T>()
        //{
        //    var response = await request;
        //    return await response.Content.ReadAsAsync<T>(CancellationToken);
        //}

        //public static Task<T> As<T>(this FluentHttpRequest request, T schema)
        //{
        //    return request.As<T>();
        //}

        //public async static Task<IEnumerable<T>> AsMany<T>(this FluentHttpRequest request)
        //{
        //    return await request.As<IEnumerable<T>>();
        //}

        //public static Task<IEnumerable<T>> AsMany<T>(this FluentHttpRequest request, T schema)
        //{
        //    return request.As<IEnumerable<T>>();
        //}

        private static async Task<string> GenerateNonSuccessMessage(HttpResponseMessage response)
        {
            var builder = new StringBuilder();
            builder.AppendLine($"Expected success status code. Got { (int)response.StatusCode } ({response.StatusCode.ToString()})");

            if (response.Content != null)
            {
                var content = await response.Content.ReadAsStringAsync();
                if (!string.IsNullOrEmpty(content))
                    builder.AppendLine($"Content in the response was: {content}");
            }

            return builder.ToString();
        }
    }
}
