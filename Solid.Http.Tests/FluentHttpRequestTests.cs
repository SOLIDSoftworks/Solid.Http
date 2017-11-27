using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FluentHttp.Tests
{
    public class FluentHttpRequestTests
    {
        private static IHttpClientCache _cache;
        private IFluentHttpClientFactory _factory;

        static FluentHttpRequestTests()
        {
            _cache = new HttpClientCache();
        }

        public FluentHttpRequestTests()
        {
            _factory = new FluentHttpClientFactory(_cache, null);
        }

        [Fact]
        public async Task ShouldGetMany()
        {
            var schema = new
            {
                UserId = 0,
                Id = 0,
                Title = string.Empty,
                Body = string.Empty
            };

            var client = _factory.Create();
            var posts = await client
                .GetAsync("https://jsonplaceholder.typicode.com/posts")
                .ExpectSuccess()
                .AsJsonArray(schema);

            Assert.NotNull(posts);
            Assert.Equal(100, posts.Count());
            foreach(var post in posts)
            {
				Assert.NotNull(post);
				Assert.NotEqual(0, post.Id);
				Assert.NotEqual(0, post.UserId);
                Assert.NotEqual(string.Empty, post.Title);
                Assert.NotEqual(string.Empty, post.Body);
            }
        }

        [Fact]
        public async Task ShouldPost()
        {
            var schema = new
            {
                UserId = 0,
                Id = 0,
                Title = string.Empty,
                Body = string.Empty
            };
            var client = _factory.Create();
            var post = await client
                .PostAsync("https://jsonplaceholder.typicode.com/posts")
                .WithJsonContent(new { UserId = 1, Body = "bar", Title = "foo" })
                .ExpectSuccess()
                .AsJson(schema);

            Assert.NotNull(post);

			Assert.Equal(101, post.Id);
			Assert.Equal(1, post.UserId);
			Assert.Equal("foo", post.Title);
			Assert.Equal("bar", post.Body);
        }
    }
}
