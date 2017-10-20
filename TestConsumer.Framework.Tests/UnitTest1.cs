using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SolidHttp.Extensions.Testing;
using SolidHttp.Extensions.Testing.MSTest;
using System.Collections.Generic;
using System.Threading.Tasks;
using SolidHttp;
using Microsoft.Extensions.Configuration.Memory;
using Microsoft.Extensions.Configuration;

namespace TestConsumer.Framework.Tests
{
    [TestClass]
    public class UnitTest1
    {
        static TestServer _server;

        [ClassInitialize]
        public static void Init(TestContext context)
        {
            var source = new MemoryConfigurationSource();
            var config = new Dictionary<string, string>();
            config.Add("ConnectionStrings:JsonPlaceholder", "https://jsonplaceholder.typicode.com");
            source.InitialData = config;
            var builder = new ConfigurationBuilder().Add(source);

            _server = new MSTestServer<Startup>().AddConfiguration(builder.Build());
        }

        [TestMethod]
        public async Task ShouldBeSuccessful()
        {
            var client = _server.CreateClient();
            await client
                .GetAsync("api/values")
                .ShouldRespondSuccessfully();
        }

        [TestMethod]
        public async Task ShouldHaveContent()
        {
            var client = _server.CreateClient();
            await client
                .GetAsync("api/values")
                .ShouldRespondWith(200).And()
                .ShouldHaveResponseHeader("content-type").WithValueStartingWith("application/json").And()
                .Should((response, asserter) =>
                {
                    asserter.IsNotNull(response.Content, "Content cannot be null");
                });
        }
    }
}
