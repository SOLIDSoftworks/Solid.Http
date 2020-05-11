using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace Solid.Http
{
    /// <summary>
    /// Extension methods for adding <see cref="HttpContent" /> to <seealso cref="HttpRequestMessage" />.
    /// </summary>
    public static class Solid_Http_SolidHttpRequestExtensions_Content
    {        
        /// <summary>
        /// Changes the boundary of multipart form data content.
        /// </summary>
        /// <param name="request">The <see cref="ISolidHttpRequest" /> that is being extended.</param>
        /// <param name="boundary">The boundary of <see cref="MultipartFormDataContent" />.</param>
        /// <returns>The <see cref="ISolidHttpRequest" /> so that additional calls can be chained.</returns>
        public static ISolidHttpRequest WithFormBoundary(this ISolidHttpRequest request, string boundary)
        {
            return request.WithMultipartContent(() => new MultipartFormDataContent(boundary));
        }

        /// <summary>
        /// Changes the sub type of the multipart content.
        /// </summary>
        /// <param name="request">The <see cref="ISolidHttpRequest" /> that is being extended.</param>
        /// <param name="subtype">The subtype of the multipart content.</param>
        /// <returns>The <see cref="ISolidHttpRequest" /> so that additional calls can be chained.</returns>
        public static ISolidHttpRequest WithSubtype(this ISolidHttpRequest request, string subtype)
        {
            return request.WithMultipartContent(() => new MultipartContent(subtype));
        }

        /// <summary>
        /// Changes the sub type and boundary of the multipart content.
        /// </summary>
        /// <param name="request">The <see cref="ISolidHttpRequest" /> that is being extended.</param>
        /// <param name="subtype">The subtype of the multipart content</param>
        /// <param name="boundary">The boundary of the multipart content</param>
        /// <returns>The <see cref="ISolidHttpRequest" /> so that additional calls can be chained.</returns>
        public static ISolidHttpRequest WithSubTypeAndBoundary(this ISolidHttpRequest request, string subtype, string boundary)
        {
            return request.WithMultipartContent(() => new MultipartContent(subtype, boundary));
        }

        /// <summary>
        /// Adds form data content to the inner <see cref="HttpRequestMessage" />.
        /// </summary>
        /// <param name="request">The <see cref="ISolidHttpRequest" /> that is being extended.</param>
        /// <param name="name">The form name of the content.</param>
        /// <param name="content">The string value of the content.</param>
        /// <returns>The <see cref="ISolidHttpRequest" /> so that additional calls can be chained.</returns>
        public static ISolidHttpRequest WithFormDataContent(this ISolidHttpRequest request, string name, string content)
        {
            return request.WithFormDataContent(name, new StringContent(content));
        }

        /// <summary>
        /// Adds form data content to the inner <see cref="HttpRequestMessage" />.
        /// </summary>
        /// <param name="request">The <see cref="ISolidHttpRequest" /> that is being extended.</param>
        /// <param name="name">The form name of the <see cref="HttpContent" />.</param>
        /// <param name="content">The <see cref="HttpContent" /> to add.</param>
        /// <returns>The <see cref="ISolidHttpRequest" /> so that additional calls can be chained.</returns>
        public static ISolidHttpRequest WithFormDataContent(this ISolidHttpRequest request, string name, HttpContent content)
        {
            var form = request.GetMultipartFormDataContent();
            form.Add(content, name);
            return request;
        }

        /// <summary>
        /// Adds form data file to the inner <see cref="HttpRequestMessage" />.
        /// </summary>
        /// <param name="request">The <see cref="ISolidHttpRequest" /> that is being extended.</param>
        /// <param name="name">The form name of the file.</param>
        /// <param name="content">The file <see cref="StreamContent" />.</param>
        /// <param name="fileName">The file name.</param>
        /// <returns>The <see cref="ISolidHttpRequest" /> so that additional calls can be chained.</returns>
        public static ISolidHttpRequest WithFormDataFile(this ISolidHttpRequest request, string name, StreamContent content, string fileName)
        {
            var form = request.GetMultipartFormDataContent();
            form.Add(content, name, fileName);
            return request;
        }

        /// <summary>
        /// Adds form data file to the inner <see cref="HttpRequestMessage" />.
        /// </summary>
        /// <param name="request">The <see cref="ISolidHttpRequest" /> that is being extended.</param>
        /// <param name="name">The form name of the file.</param>
        /// <param name="content">The file <see cref="ByteArrayContent" />.</param>
        /// <param name="fileName">The file name.</param>
        /// <returns>The <see cref="ISolidHttpRequest" /> so that additional calls can be chained.</returns>
        public static ISolidHttpRequest WithFormDataFile(this ISolidHttpRequest request, string name, ByteArrayContent content, string fileName)
        {
            var form = request.GetMultipartFormDataContent();
            form.Add(content, name, fileName);
            return request;
        }

        /// <summary>
        /// Adds <see cref="HttpContent" /> to the inner <seealso cref="HttpRequestMessage" />.
        /// <para>If there is already <see cref="HttpContent" /> on the <seealso cref="HttpRequestMessage" />, it makes the request multipart.</para>
        /// </summary>
        /// <param name="request">The <see cref="ISolidHttpRequest" /> that is being extended.</param>
        /// <param name="content">The <see cref="HttpContent" /> to add to the <seealso cref="HttpRequestMessage" />.await</param>
        /// <returns>The <see cref="ISolidHttpRequest" /> so that additional calls can be chained.</returns>
        public static ISolidHttpRequest WithContent(this ISolidHttpRequest request, HttpContent content)
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

        private static MultipartFormDataContent GetMultipartFormDataContent(this ISolidHttpRequest request)
        {
            var multipart = request.BaseRequest.Content as MultipartFormDataContent;
            if (multipart == null)
                multipart = request.WithMultipartContent(() => new MultipartFormDataContent()).BaseRequest.Content as MultipartFormDataContent;

            return multipart;
        }

        private static ISolidHttpRequest WithMultipartContent(this ISolidHttpRequest request, Func<MultipartContent> create)
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
            foreach (var c in contents)
            {
                m.Add(c);
            }
            request.BaseRequest.Content = m;
            return request;
        }
    }
}
