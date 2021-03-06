using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Solid.Http.Json.Tests
{
    public class JsonTests
    {
        [Fact]
        public async Task ShouldDeserializeSingleJsonObject()
        {
            var services = new ServiceCollection();
            services.AddSolidHttpCore(b => b.AddNewtonsoftJson());
            var root = services.BuildServiceProvider();
            using (var scope = root.CreateScope())
            {
                var provider = scope.ServiceProvider;
                var factory = provider.GetService<ISolidHttpClientFactory>();
                var client = factory.Create();
                var post = await client
                    .GetAsync("https://jsonplaceholder.typicode.com/posts/1")
                    .As<Post>()
                ;

                Assert.NotNull(post);
                Assert.NotNull(post.Title);
                Assert.NotNull(post.Body);
                Assert.NotEqual(0, post.Id);
                Assert.NotEqual(0, post.UserId);
            }
        }

        [Fact]
        public async Task ShouldDeserializeJsonArray()
        {
            var services = new ServiceCollection();
            services.AddSolidHttpCore(b => b.AddNewtonsoftJson());
            var root = services.BuildServiceProvider();
            using (var scope = root.CreateScope())
            {
                var provider = scope.ServiceProvider;
                var factory = provider.GetService<ISolidHttpClientFactory>();
                var client = factory.Create();
                var posts = await client
                    .GetAsync("https://jsonplaceholder.typicode.com/posts")
                    .AsMany<Post>()
                ;

                Assert.NotNull(posts);
                Assert.NotEmpty(posts);

                var post = posts.First();

                Assert.NotNull(post);
                Assert.NotNull(post.Title);
                Assert.NotNull(post.Body);
                Assert.NotEqual(0, post.Id);
                Assert.NotEqual(0, post.UserId);
            }
        }

        class Post
        {
            public int Id { get; set; }
            public int UserId { get; set; }
            public string Title { get; set; }
            public string Body { get; set; }
        }
    }
}
