using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Solid.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Diagnostics;


namespace TestConsumer.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private ISolidHttpClientFactory _factory;

        public ValuesController(ISolidHttpClientFactory factory)
        {
            _factory = factory;
        }
        // GET api/values
        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            var client = _factory.CreateUsingConnectionString("JsonPlaceholder");
            var posts = await client
                .GetAsync("posts", cancellationToken)
                .On(HttpStatusCode.OK, async (response) =>
                {
                    var r = await client.GetAsync("posts");
                    Debug.WriteLine(r.StatusCode);
                })
                .AsMany(new
                {
                    UserId = 0,
                    Id = 0,
                    Title = string.Empty,
                    Body = string.Empty
                });

            return Ok(posts);
        }
    }
}
