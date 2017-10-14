# SolidHttp 
[![solidsoftworks MyGet Build Status](https://www.myget.org/BuildSource/Badge/solidsoftworks?identifier=a3ce4e9e-bc86-4795-a1fb-3fe77c9662f6)](https://www.myget.org/)

SolidHttp is a library to simplify http calls in C# for .net standard 2.0. It's designed to be async and fully extendable.

## Initialization
SolidHttp is designed to work with the Startup class and IServiceCollection.

### Basic initialization
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSolidHttp();
        }
    }

### Intermediate initialization
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddSolidHttp()
                .Configure(options =>
                {
                    options.Events.OnRequestCreated += (sender, args) =>
                    {
                        args.Request.WithHeader("x-default-header", "default");
                    };
                });
        }
    }

### Manual initialization
    public ISolidHttpClientFactory Initialize()
    {
        var builder = new SolidHttpClientFactoryBuilder();
        return builder.Build();
    }
    
## Use
When working with SolidHttp, you use ISolidHttpClientFactory to create a SolidHttpClient. This SolidHttpClient then creates a SolidHttpRequest. The SolidHttpRequest itself is awaitable so any extension method that returns SolidHttpRequest can be awaited.

### Basic example
    [Route("api/[controller]")]
    public class SomeController : Controller
    {
        private ISolidHttpClientFactory _factory;
        public SomeClass(ISolidHttpClientFactory factory)
        {
            // The SolidHttpClientFactory class would be injected here
            _factory = factory;
        }

        public Task<HttpResponseMessage> GetSomethingAsync(CancellationToken cancellationToken)
        {
            var client = _factory.Create();
            return await client
                .GetAsync("https://jsonplaceholder.typicode.com/posts", cancellationToken);
        }
    }

### Intermediate example
    [Route("api/[controller]")]
    public class PostsController : Controller
    {
        private ISolidHttpClientFactory _factory;
        public PostsController(ISolidHttpClientFactory factory)
        {
            // The SolidHttpClientFactory class would be injected here
            _factory = factory;
        }

        public Task<IActionResult> PutAsync(int id, Post body, CancellationToken cancellationToken)
        {
            var client = _factory.CreateWithBaseAddress("https://jsonplaceholder.typicode.com");
            var json = JsonConvert.SerializeObject(body);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var post = await client
                .PutAsync("posts/{id}", cancellationToken)
                .WithContent(content)
                .WithNamedParameter("id", id)
                .ExpectSuccess()
                .As<Post>();
            return Ok(post);
        }
    }

### Extension example
    public static class StandardHeaderExample
    {
        public static SolidHttpRequest AddStandardHeader(this SolidHttpRequest request)
        {
            return request.WithHeader("x-standard-header", "standard");
        }
    }