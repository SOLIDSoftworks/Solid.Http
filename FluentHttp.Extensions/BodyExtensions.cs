using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace FluentHttp
{
    public static class BodyExtensions
    {
        public static FluentHttpRequest AsMultipart(this FluentHttpRequest request)
        {
            if (IsCorrectMultipart(request.BaseRequest.Content, null, null)) return request;

            var multipart = new VerboseMultipartContent();
            multipart.AddRange(GetCurrentContent(request));
            request.BaseRequest.Content = multipart;
            return request;
        }
        public static FluentHttpRequest AsMultipart(this FluentHttpRequest request, string multipartSubType)
        {
            if (IsCorrectMultipart(request.BaseRequest.Content, multipartSubType, null)) return request;

            var multipart = new VerboseMultipartContent(multipartSubType);
            multipart.AddRange(GetCurrentContent(request));
            request.BaseRequest.Content = multipart;
            return request;
        }
        public static FluentHttpRequest AsMultipart(this FluentHttpRequest request, string multipartSubType, string boundary)
        {
            if (IsCorrectMultipart(request.BaseRequest.Content, multipartSubType, boundary)) return request;

            var multipart = new VerboseMultipartContent(multipartSubType, boundary);
            multipart.AddRange(GetCurrentContent(request));
            request.BaseRequest.Content = multipart;
            return request;
        }

        public static FluentHttpRequest WithContent(this FluentHttpRequest request, HttpContent content)
        {
            if(request.BaseRequest.Content == null)
            {
                request.BaseRequest.Content = content;
                return request;
            }

            if(!(request.BaseRequest.Content is MultipartContent))
                request.AsMultipart();

            var multipart = request.BaseRequest.Content as MultipartContent;
            multipart.Add(content);
            return request;
        }

        private static bool IsCorrectMultipart(HttpContent content, string subType, string boundary)
        {
            if (content == null) return false;
            var verbose = content as VerboseMultipartContent;
            if (verbose == null) return false;
            if (verbose.SubType != subType || verbose.Boundary != boundary) return false;

            return true;
        }

        private static IEnumerable<HttpContent> GetCurrentContent(FluentHttpRequest request)
        {
            if (request.BaseRequest.Content == null) yield break;

            var multipart = request.BaseRequest.Content as MultipartContent;
            if (multipart == null)
                yield return request.BaseRequest.Content;

            foreach (var c in multipart)
                yield return c;
        }

        class VerboseMultipartContent : MultipartContent
        {
            public VerboseMultipartContent() : base() { }
            public VerboseMultipartContent(string subType) : base(subType)
            {
                SubType = subType;
            }

            public VerboseMultipartContent(string subType, string boundary) : base(subType, boundary)
            {
                SubType = subType;
                Boundary = boundary;
            }

            public string SubType { get; private set; }
            public string Boundary { get; private set; }

            public void AddRange(IEnumerable<HttpContent> content)
            {
                foreach(var c in content)
                {
                    Add(c);
                }
            }
        }
    }
}
