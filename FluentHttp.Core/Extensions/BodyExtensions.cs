using System;
using System.Linq;
using System.Net.Http;

namespace FluentHttp
{
    public static class BodyExtensions
    {
        public static FluentHttpRequest WithFormBoundary(this FluentHttpRequest request, string boundary)
        {
            return request.WithMultipartContent(() => new MultipartFormDataContent(boundary));
        }
        public static FluentHttpRequest WithSubtype(this FluentHttpRequest request, string subtype)
        {
            return request.WithMultipartContent(() => new MultipartContent(subtype));
        }

        public static FluentHttpRequest WithSubTypeAndBoundary(this FluentHttpRequest request, string subtype, string boundary)
        {
            return request.WithMultipartContent(() => new MultipartContent(subtype, boundary));
        }

        public static FluentHttpRequest WithFormDataContent(this FluentHttpRequest request, string name, string content)
        {
            return request.WithFormDataContent(name, new StringContent(content));
        }

        public static FluentHttpRequest WithFormDataContent(this FluentHttpRequest request, string name, HttpContent content)
        {
            var form = request.GetMultipartFormDataContent();
            form.Add(content, name);
            return request;
        }

        public static FluentHttpRequest WithFormDataFile(this FluentHttpRequest request, string name, StreamContent content, string fileName)
        {
            var form = request.GetMultipartFormDataContent();
            form.Add(content, name, fileName);
            return request;
        }

        public static FluentHttpRequest WithFormDataFile(this FluentHttpRequest request, string name, ByteArrayContent content, string fileName)
        {
            var form = request.GetMultipartFormDataContent();
            form.Add(content, name, fileName);
            return request;
        }

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
            if (request.BaseRequest.Content == null)
                request.BaseRequest.Content = new MultipartFormDataContent();

            var multipart = request.BaseRequest.Content as MultipartFormDataContent;
            if (multipart == null)
                throw new InvalidOperationException(); // TODO: add message

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
