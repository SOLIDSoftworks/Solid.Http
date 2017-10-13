using System;
using System.Linq;
using System.Net.Http;

namespace FluentHttp
{
    /// <summary>
    /// BodyExtensions
    /// </summary>
    public static class BodyExtensions
    {
        /// <summary>
        /// Changes the boundary of the multipart form data content
        /// </summary>
        /// <param name="request">The FluentHttpRequest</param>
        /// <param name="boundary">The boundary of the multipart form data content</param>
        /// <returns>FluentHttpRequest</returns>
        public static FluentHttpRequest WithFormBoundary(this FluentHttpRequest request, string boundary)
        {
            return request.WithMultipartContent(() => new MultipartFormDataContent(boundary));
        }

        /// <summary>
        /// Changes the sub type of the multipart content
        /// </summary>
        /// <param name="request">The FluentHttpRequest</param>
        /// <param name="subtype">The subtype of the multipart content</param>
        /// <returns>FluentHttpRequest</returns>
        public static FluentHttpRequest WithSubtype(this FluentHttpRequest request, string subtype)
        {
            return request.WithMultipartContent(() => new MultipartContent(subtype));
        }

        /// <summary>
        /// Changes the sub type and boundary of the multipart content
        /// </summary>
        /// <param name="request">The FluentHttpRequest</param>
        /// <param name="subtype">The subtype of the multipart content</param>
        /// <param name="boundary">The boundary of the multipart content</param>
        /// <returns>FluentHttpRequest</returns>
        public static FluentHttpRequest WithSubTypeAndBoundary(this FluentHttpRequest request, string subtype, string boundary)
        {
            return request.WithMultipartContent(() => new MultipartContent(subtype, boundary));
        }

        /// <summary>
        /// Adds form data content to the request
        /// </summary>
        /// <param name="request">The FluentHttpRequest</param>
        /// <param name="name">The form name of the content</param>
        /// <param name="content">The string value of the content</param>
        /// <returns>FluentHttpRequest</returns>
        public static FluentHttpRequest WithFormDataContent(this FluentHttpRequest request, string name, string content)
        {
            return request.WithFormDataContent(name, new StringContent(content));
        }

        /// <summary>
        /// Adds form data content to the request
        /// </summary>
        /// <param name="request">The FluentHttpRequest</param>
        /// <param name="name">The form name of the content</param>
        /// <param name="content">The HttpContent</param>
        /// <returns>FluentHttpRequest</returns>
        public static FluentHttpRequest WithFormDataContent(this FluentHttpRequest request, string name, HttpContent content)
        {
            var form = request.GetMultipartFormDataContent();
            form.Add(content, name);
            return request;
        }

        /// <summary>
        /// Adds form data file to request
        /// </summary>
        /// <param name="request">The FluentHttpRequest</param>
        /// <param name="name">The form name of the file</param>
        /// <param name="content">The file StreamContent</param>
        /// <param name="fileName">The file name</param>
        /// <returns>FluentHttpRequest</returns>
        public static FluentHttpRequest WithFormDataFile(this FluentHttpRequest request, string name, StreamContent content, string fileName)
        {
            var form = request.GetMultipartFormDataContent();
            form.Add(content, name, fileName);
            return request;
        }

        /// <summary>
        /// Adds form data file to request
        /// </summary>
        /// <param name="request">The FluentHttpRequest</param>
        /// <param name="name">The form name of the file</param>
        /// <param name="content">The file ByteArrayContent</param>
        /// <param name="fileName">The file name</param>
        /// <returns>FluentHttpRequest</returns>
        public static FluentHttpRequest WithFormDataFile(this FluentHttpRequest request, string name, ByteArrayContent content, string fileName)
        {
            var form = request.GetMultipartFormDataContent();
            form.Add(content, name, fileName);
            return request;
        }

        /// <summary>
        /// Adds HttpContent to the request
        /// <para>If there is already HttpContent on the request, it makes the request multipart</para>
        /// </summary>
        /// <param name="request">The FluentHttpRequest</param>
        /// <param name="content">The HttpContent</param>
        /// <returns>FluentHttpRequest</returns>
        public static FluentHttpRequest WithContent(this FluentHttpRequest request, HttpContent content)
        {
            if (request.BaseRequest.Content == null)
            {
                request.BaseRequest.Content = content;
                return request;
            }
            var multipart = request.WithMultipartContent(() => new MultipartContent()).BaseRequest.Content as MultipartContent;
            multipart.Add(content);
            return request;
        }

        private static MultipartFormDataContent GetMultipartFormDataContent(this FluentHttpRequest request)
        {
            var multipart = request.BaseRequest.Content as MultipartFormDataContent;
            if (multipart == null)
                multipart = request.WithMultipartContent(() => new MultipartFormDataContent()).BaseRequest.Content as MultipartFormDataContent;

            return multipart;
        }

        private static FluentHttpRequest WithMultipartContent(this FluentHttpRequest request, Func<MultipartContent> create)
        {
            var content = request.BaseRequest.Content;
            var multipart = content as MultipartContent;
            var contents = Enumerable.Empty<HttpContent>();
            if (multipart != null)
                contents = multipart;
            else if (content != null)
                contents = new[] { content };

            var m = create();
            // TODO: Make sure the headers aren't gonna be a problem
            foreach(var c in contents)
            {
                m.Add(c);
            }
            request.BaseRequest.Content = m;
            return request;
        }
    }
}
