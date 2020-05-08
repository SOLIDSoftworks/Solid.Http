using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Solid.Http.Core.Tests
{
    public class HttpContentTests
    {
        [Fact]
        public async Task ShouldAllowMultipleStreamRead()
        {
            var services = new ServiceCollection();
            services.AddSolidHttpCore();
            var provider = services.BuildServiceProvider();
            var factory = provider.GetService<ISolidHttpClientFactory>();
            var client = factory.Create();
            var request = client
                .GetAsync("https://jsonplaceholder.typicode.com/posts/1");
            var post = await request.AsText();
            post = await request.AsText();
        }
    }
}
