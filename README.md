# FluentHttp

FluentHttp is a library to simplify http calls in C#. It's designed to be async and fully extendable.

## Initialization
FluentHttp is designed to work with the Startup class and IServiceCollection.

### Basic initialization
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddFluentHttp();
        }
    }

### Intermediate initialization
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddFluentHttp()
                .Configure(options =>
                {
                    options.FactoryEvents.OnClientCreated += (sender, args) =>
                    {
                        args.Client.OnRequestCreated += (s, a) =>
                        {
                            a.Request.WithHeader("x-default-header", "default");
                        };
                    };
                });
        }
    }

### Manual initialization
    public IFluentHttpClientFactory Initialize()
    {
        var builder = new FluentHttpClientFactoryBuilder();
        return builder.Build();
    }
    
## Use
When working with FluentHttp, you use IFluentHttpClientFactory to create a FluentHttpClient. This FluentHttpClient then creates a FluentHttpRequest. The FluentHttpRequest itself is awaitable so any extension method that returns FluentHttpRequest can be awaited.

### Basic example
    [Route("api/[controller]")]
    public class SomeController : Controller
    {
        private IFluentHttpClientFactory _factory;
        public SomeClass(IFluentHttpClientFactory factory)
        {
            // The FluentHttpClientFactory class would be injected here
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
        private IFluentHttpClientFactory _factory;
        public PostsController(IFluentHttpClientFactory factory)
        {
            // The FluentHttpClientFactory class would be injected here
            _factory = factory;
        }

        public Task<IActionResult> PutAsync(int id, Post body, CancellationToken cancellationToken)
        {
            var client = _factory.Create("https://jsonplaceholder.typicode.com");
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
        public static FluentHttpRequest AddStandardHeader(this FluentHttpRequest request)
        {
            return request.WithHeader("x-standard-header", "standard");
        }
    }