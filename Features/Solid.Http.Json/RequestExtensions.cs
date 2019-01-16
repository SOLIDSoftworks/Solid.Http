using Newtonsoft.Json;
using Solid.Http.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Solid.Http.Abstractions
{
    /// <summary>
    /// Request extensions for json content
    /// </summary>
    public static class RequestExtensions
    {
        /// <summary>
        /// Adds StringContent containing a json string of the supplied body object
        /// </summary>
        /// <typeparam name="T">The type of body</typeparam>
        /// <param name="request">The SolidHttpRequest</param>
        /// <param name="body">The request body object</param>
        /// <param name="settings">(Optional) JsonSerializerSettings to use to serialize the body object</param>
        /// <returns>SolidHttpRequest</returns>
        public static ISolidHttpRequest WithJsonContent<T>(this ISolidHttpRequest request, T body, JsonSerializerSettings settings = null)
        {            
            var json = JsonConvert.SerializeObject(body, settings ?? request.GetJsonSerializerSettings());
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            return request.WithContent(content);
        }
    }
}
