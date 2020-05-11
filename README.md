# Solid.Http [![License](https://img.shields.io/github/license/mashape/apistatus.svg)](https://en.wikipedia.org/wiki/MIT_License) [![Build status](https://ci.appveyor.com/api/projects/status/w3071ev5pc3r4bob/branch/master?svg=true)](https://www.appveyor.com/) ![Solid.Http on nuget](https://img.shields.io/nuget/vpre/Solid.Http)

_Solid.Http_ is a library to simplify http calls in C# for .net standard 2.0. It's designed to be async and fully extendable.

## Packages
* [Solid.Http.Core](src/Core/README.md)
* [Solid.Http.Json](src/Extensions/Json/README.md)
* [Solid.Http](README.md)
  * This package is a shortcut for adding Solid.Http.Core and Solid.Http.Json.
* [Solid.Http.NewtonsoftJson](src/Extensions/Json/README.md#solidhttpnewtonsoftjson)
* [Solid.Http.Zip](src/Extensions/Zip/README.md)
* [Solid.Http.Xml](src/Extensions/Xml/README.md)

## Basic usage
The basic package is _Solid.Http_ which includes _Solid.Http.Core_ and _Solid.Http.Json_.

```cli
> dotnet add package Solid.Http
```

### Initialization
Adding _Solid.Http_ is quite simple when you just need the fluent interface and JSON.

```csharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddSolidHttp();
}
```

#### Global event handlers
You can also register event handlers during initialization.

```csharp
public void ConfigureServices(IServiceCollection services)
{
    services
        .AddSolidHttp(builder => 
        {
            builder
                .Configure(options =>
                {
                    options.OnClientCreated((services, client) =>
                    {
                         // your client created handler here.
                    });
                    options.OnRequestCreated((services, request) =>
                    {
                         // your request created handler here.
                    });
                    options.OnHttpRequest(async (services, httpRequest) =>
                    {
                        // your http request handler here.
                    });
                    options.OnHttpResponse(async (services, httpResponse) =>
                    {
                        // your http response handler here.
                    });
                })
            ;
        })
    ;
}
```

> More about event handlers in the [important interfaces section of the Solid.Http.Core README](src/Core/README.md#important-interfaces).

### Usage
You can inject _ISolidHttpClientFactory_ using dependency injection and then use it to create an _ISolidHttpClient_. _ISolidHttpClient_ is used to create an _ISolidHttpRequest_ which then has a fully async fluent interface for performing an HTTP request.

```csharp
[Route("[controller]")]
public class PostsController : Controller
{
    private ISolidHttpClient _client;

    public PostsController(ISolidHttpClientFactory factory)
    {
        _client = factory.CreateWithBaseAddress("https://jsonplaceholder.typicode.com");
    }

    [HttpGet]
    public async Task<IActionResult> GetAsync(CancellationToken cancellationToken)
    {
        var posts = await _client
            .GetAsync("posts", cancellationToken)
            .AsMany<Post>()
        ;

        return Ok(posts);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync([FromRoute] int id, [FromBody] Post body)
    {
        await _client
            .PutAsync("posts/{id}")
            .WithNamedParameter("id", id)
            .WithJsonContent(body)
            .ExpectSuccess()
        ;
        return Ok(body);
    }

    class Post
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
    }
}
```