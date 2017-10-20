using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace SolidHttp.Core.Tests
{
    public class SolidHttpClientFactoryBuilderTests
    {
        [Fact]
        public void ShouldRunOnClientCreated()
        {
            var builder = new SolidHttpClientFactoryBuilder();
            builder.Setup(setup =>
            {
                setup.Configure(options =>
                {
                    options.Events.OnClientCreated += (sender, args) =>
                    {
                        args.Client.AddProperty("success", true);
                    };
                });
            });
            var factory = builder.Build();
            var client = factory.Create();

            Assert.True(client.GetProperty<bool>("success"));
        }
    }
}
