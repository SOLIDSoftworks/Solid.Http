using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Solid.Http
{
    /// <summary>
    /// ResponseExtensions
    /// </summary>
    public static class ResponseExtensions
    {
        /// <summary>
        /// Deserializes the response content using a specified deserializer
        /// </summary>
        /// <typeparam name="T">The type to deserialize to</typeparam>
        /// <param name="request">The SolidHttpRequest</param>
        /// <param name="deserialize">The deserialization method</param>
        /// <returns>Task of type T</returns>
        public static async Task<T> As<T>(this SolidHttpRequest request, Func<HttpContent, Task<T>> deserialize)
        {
            var content = await request.GetContentAsync();
            if (content == null) return default(T); // should we maybe throw an exception if there is no content?
            return await deserialize(content);
        }

        /// <summary>
        /// Deserializes the response content as the specified anonymous type
        /// </summary>
        /// <typeparam name="T">The type to deserialize to</typeparam>
        /// <param name="request">The SolidHttpRequest</param>
        /// <param name="anonymous">An anonumous type to infer T</param>
        /// <returns>Task of type T</returns>
        public static async Task<T> As<T>(this SolidHttpRequest request, T anonymous)
        {
            return await request.As<T>();
        }

        /// <summary>
        /// Deserializes the response content as an array of the specified anonymous type
        /// </summary>
        /// <typeparam name="T">The type to deserialize to</typeparam>
        /// <param name="request">The SolidHttpRequest</param>
        /// <param name="anonymous"></param>
        /// <returns>Task of type IEnumerable&lt;T&gt;</returns>
        public static async Task<IEnumerable<T>> AsMany<T>(this SolidHttpRequest request, T anonymous)
        {
            return await request.As<IEnumerable<T>>();
        }

        /// <summary>
        /// Deserializes the response content as an array of type T
        /// </summary>
        /// <typeparam name="T">The type to deserialize to</typeparam>
        /// <param name="request">The SolidHttpRequest</param>
        /// <returns>Task of type IEnumerable&lt;T&gt;</returns>
        public static async Task<IEnumerable<T>> AsMany<T>(this SolidHttpRequest request)
        {
            return await request.As<IEnumerable<T>>();
        }

        /// <summary>
        /// Deserializes the response content
        /// </summary>
        /// <typeparam name="T">The type to deserialize to</typeparam>
        /// <param name="request">The SolidHttpRequest</param>
        /// <returns>Task of type T</returns>
        public static async Task<T> As<T>(this SolidHttpRequest request)
        {
            var content = await request.GetContentAsync();
            if (content == null) return default(T); // should we maybe throw an exception if there is no content?

            var mime = content?.Headers?.ContentType?.MediaType;

            var deserializer = request.Client.Deserializers.FirstOrDefault(d => d.CanDeserialize(mime));
            if (deserializer == null)
                throw new InvalidOperationException($"Cannot deserialize {mime} response as {typeof(T).FullName}");
            return await deserializer.DeserializeAsync<T>(content);
        }

        /// <summary>
        /// Returns the response content as text
        /// </summary>
        /// <param name="request">The SolidHttpRequest</param>
        /// <returns>Task of type string</returns>
        public static async Task<string> AsText(this SolidHttpRequest request)
        {
            return await request.As(async content => await content.ReadAsStringAsync());
        }

        /// <summary>
        /// Expect a success status code
        /// <para>If a non-success status code is received, an InvalidOperationException is thrown</para>
        /// </summary>        
        /// <param name="request">The SolidHttpRequest</param>
        /// <returns>SolidHttpRequest</returns>
        public static SolidHttpRequest ExpectSuccess(this SolidHttpRequest request)
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

        /// <summary>
        /// Map a handler to a specific http status code
        /// </summary>
        /// <param name="request">The SolidHttpRequest</param>
        /// <param name="code">The http status code</param>
        /// <param name="handler">The handler</param>
        /// <returns>SolidHttpRequest</returns>
        public static SolidHttpRequest On(this SolidHttpRequest request, HttpStatusCode code, Action<HttpResponseMessage> handler)
        {
            request.OnResponse += (sender, args) =>
            {
                if (args.Response.StatusCode == code)
                    handler(args.Response);
            };
            return request;
        }
        
        /// <summary>
        /// Map an async handler to a specific http status code
        /// </summary>
        /// <param name="request">The SolidHttpRequest</param>
        /// <param name="code">The http status code</param>
        /// <param name="handler">The async handler</param>
        /// <returns>SolidHttpRequest</returns>
        public static SolidHttpRequest On(this SolidHttpRequest request, HttpStatusCode code, Func<HttpResponseMessage, Task> handler)
        {
            request.OnResponse += (sender, args) =>
            {
                var done = false;
                if (args.Response.StatusCode == code)
                    handler(args.Response).ContinueWith(t =>
                    {
                        done = true;
                    });
                else
                    done = true;

                System.Threading.SpinWait.SpinUntil(() => done);
            };
            return request;
        }        

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

        private static async Task<HttpContent> GetContentAsync(this SolidHttpRequest request)
        {
            var response = await request;
            return response.Content;
        }
    }
}
