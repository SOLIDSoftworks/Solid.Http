using Microsoft.Extensions.DependencyInjection;
using Solid.Http.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Solid.Http.Tests
{
    public class HttpContentTests
    {
        [Fact]
        public async Task ShouldAllowMultipleStreamRead()
        {
            var services = new ServiceCollection();
            services.AddSolidHttp();
            var root = services.BuildServiceProvider();
            using (var scope = root.CreateScope())
            {
                var provider = scope.ServiceProvider;
                var factory = provider.GetService<ISolidHttpClientFactory>();
                var client = factory.Create();
                var response = client
                    .GetAsync("https://jsonplaceholder.typicode.com/posts/1");
                var schema = new
                {
                    Id = 0,
                    UserId = 0,
                    Title = string.Empty,
                    Body = string.Empty
                };
                var post = await response
                    .As(schema);

                post = await response.As(schema);
            }
        }
    }
}
