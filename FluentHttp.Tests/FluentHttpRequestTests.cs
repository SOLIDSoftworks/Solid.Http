using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;

namespace FluentHttp.Tests
{
    [TestClass]
    public class FluentHttpRequestTests
    {
        private static IHttpClientCache _cache;
        private IFluentHttpClientFactory _factory;

        [ClassInitialize]
        public static void InitializeClass(TestContext context)
        {
            _cache = new HttpClientCache();
        }

        [TestInitialize]
        public void Initialize()
        {
            _factory = new FluentHttpClientFactory(_cache, null);
        }

        [TestMethod]
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

            posts.Should().NotBeNull();
            posts.Should().HaveCount(100);
            posts.ToList().ForEach(p =>
            {
                p.Id.Should().NotBe(0);
                p.UserId.Should().NotBe(0);
                p.Body.Should().NotBe(string.Empty);
                p.Title.Should().NotBe(string.Empty);
            });
        }

        [TestMethod]
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

            post.Should().NotBeNull();
            post.Id.Should().Be(101);
            post.UserId.Should().Be(1);
            post.Body.Should().Be("bar");
            post.Title.Should().Be("foo");
        }
    }
}
