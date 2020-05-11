# Solid.Http JSON extensions

There are two JSON extensions for _Solid.Http.Core_. One extension, _Solid.Http.Json_ uses _System.Text.Json_ for serialization/deserialization. The other extension, _Solid.Http.NewtonsoftJson_, uses _Newtonsoft.Json_ for serialization/deserialization.

## Solid.Http.Json

_Solid.Http.Json_ uses _System.Text.Json_ internally.

```cli
> dotnet add package Solid.Http.Json
```

### Initialization with Solid.Http.Core
If you're using _Solid.Http.Core_ directly, you can add _Solid.Http.Json_ using the builder.

```csharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddSolidHttpCore(builder => 
    {
        builder.AddJson(options => 
        {
            options.SerializerOptions = new JsonSerializerOptions
            {
                // configure your JsonSerializerOptions here
            };
        });
    });
}
```

> If you're using _Solid.Http_, then the _AddJson()_ method on the builder is called internally. You can configure the JsonSerializerOptions in one of two ways. You can call _AddJson()_ on the builder with no adverse consequences, or you can call _ConfigureSolidHttpJson(o => {})_ on the IServiceCollection directly.

### Http request body serialization

Included in the package is an extension method that makes it easier to add JSON content to an HTTP request.

```csharp
public async Task<Post> CreateAsync(Post post)
{
    var options = new JsonSerializerOptions
    {
        // configure your options here.
    };
    await _client
        .PostAsync("posts")
        // contentType and options are optional
        // If options are omitted, the options that are specified in the SolidHttpJsonOptions are used.
        .WithJsonContent(post, contentType: "application/json", options: options)
    ;
    return post;
}
```

### Http response body deserialization

The _As_ methods that are included with _Solid.Http.Core_ will use the configured JsonSerializerOptions to deserialize the received response body.

```csharp
public async Task<Post> GetAsync(int id)
{
    return await _client
        .GetAsync("posts/{id}")
        .WithNamedParameter("id", id)
        .ExpectSuccess()
        .As<Post>()
    ;
}
```

You can also specify JsonSerializerOptions if a specific API endpoint differs from the default in how it serializes it's entities.

```csharp
public async Task<Post> GetAsync(int id)
{
    var options = new JsonSerializerOptions
    {
        // configure your options here.
    };
    return await _client
        .GetAsync("posts/{id}")
        .WithNamedParameter("id", id)
        .ExpectSuccess()
        .As<Post>(options)
    ;
}
```

## Solid.Http.NewtonsoftJson

_Solid.Http.NewtonsoftJson_ uses _Newtonsoft.Json_ internally.

```cli
> dotnet add package Solid.Http.NewtonsoftJson
```

### Initialization with Solid.Http.Core
You can add _Solid.Http.NewtonsoftJson_ using the builder.

```csharp
public void ConfigureServices(IServiceCollection services)
{
    // AddSolidHttpCore could be replaced with AddSolidHttp here
    services.AddSolidHttpCore(builder => 
    {
        builder.AddNewtonsoftJson(options => 
        {
            options.SerializerSettings = new JsonSerializerSettings
            {
                // configure your JsonSerializerSettings here
            };
        });
    });
}
```

> If _Solid.Http.Json_ has beed previously added, _Solid.Http.NewtonsoftJson_ will replace it for default deserialization.

### Http request body serialization

Included in the package is an extension method that makes it easier to add JSON content to an HTTP request.

```csharp
public async Task<Post> CreateAsync(Post post)
{
    var settings = new JsonSerializerSettings
    {
        // configure your settings here.
    };
    await _client
        .PostAsync("posts")
        // contentType and settings are optional
        // If settings are omitted, the settings that are specified in the SolidHttpNewtonsoftJsonOptions are used.
        .WithNewtonsoftJsonContent(post, contentType: "application/json", settings: settings)
    ;
    return post;
}
```

### Http response body deserialization

The _As_ methods that are included with _Solid.Http.Core_ will use the configured JsonSerializerSettings to deserialize the received response body.

```csharp
public async Task<Post> GetAsync(int id)
{
    return await _client
        .GetAsync("posts/{id}")
        .WithNamedParameter("id", id)
        .ExpectSuccess()
        .As<Post>()
    ;
}
```

You can also specify JsonSerializerSettings if a specific API endpoint differs from the default in how it serializes it's entities.

```csharp
public async Task<Post> GetAsync(int id)
{
    var settings = new JsonSerializerSettings
    {
        // configure your settings here.
    };
    return await _client
        .GetAsync("posts/{id}")
        .WithNamedParameter("id", id)
        .ExpectSuccess()
        .As<Post>(settings)
    ;
}
```
