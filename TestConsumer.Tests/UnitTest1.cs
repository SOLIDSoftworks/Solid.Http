using Microsoft.VisualStudio.TestTools.UnitTesting;
using SolidHttp;
using SolidHttp.Extensions.Testing;
using SolidHttp.Extensions.Testing.MSTest;
using System.Threading.Tasks;

namespace TestConsumer.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public async Task TestMethod1()
        {
            using (var server = new MSTestServer<Startup>())
            {
                var client = server.CreateClient();
                await client
                    .GetAsync("api/values")
                    .ShouldRespondSuccessfully();
            }
        }
    }
}
