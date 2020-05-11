# Solid.Http XML extensions

There is currently on XML extension for _Solid.Http.Core_. The extension uses the DataContractSerializer for serialization/deserialization.

## Solid.Http.Xml

_Solid.Http.Xml_ uses _DataContractSerializer_ internally.

```cli
> dotnet add package Solid.Http.Xml
```

### Initialization with Solid.Http.Core
You can add _Solid.Http.Xml_ using the builder.

```csharp
public void ConfigureServices(IServiceCollection services)
{
    // This call to AddSolidHttpCore could be replaced with AddSolidHttp
    services.AddSolidHttpCore(builder => 
    {
        builder.AddXml(options => 
        {
            options.SerializerSettings = new DataContractSerializerSettings
            {
                // configure your DataContractSerializerSettings here
            };
        });
    });
}
```

### Http request body serialization

Included in the package is an extension method that makes it easier to add XML content to an HTTP request.

```csharp
public async Task<Post> CreateAsync(Post post)
{
    var settings = new DataContractSerializerSettings
    {
        // configure your settings here.
    };
    await _client
        .PostAsync("posts")
        // contentType and settings are optional
        // If settings are omitted, the settings that are specified in the SolidHttpXmlOptions are used.
        .WithXmlContent(post, contentType: "application/xml", settings: settings)
    ;
    return post;
}
```

### Http response body deserialization

The _As_ methods that are included with _Solid.Http.Core_ will use the configured DataContractSerializerSettings to deserialize the received response body.

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

You can also specify DataContractSerializerSettings if a specific API endpoint differs from the default in how it serializes it's entities.

```csharp
public async Task<Post> GetAsync(int id)
{
    var settings = new DataContractSerializerSettings
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
