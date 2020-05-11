# Solid.Http XML extensions

There is currently one ZIP extension for _Solid.Http.Core_. The extension uses the _System.IO.Compression_ for deserialization. This package is useful if you need to request a zip archive and manipulate it programmatically.

## Solid.Http.Xml

_Solid.Http.Zip_ uses _System.IO.Compression_ internally.

```cli
> dotnet add package Solid.Http.Zip
```

### Initialization with Solid.Http.Core
You can add _Solid.Http.Zip_ using the builder.

```csharp
public void ConfigureServices(IServiceCollection services)
{
    // This call to AddSolidHttpCore could be replaced with AddSolidHttp
    services.AddSolidHttpCore(builder => 
    {
        builder.AddZip(options => 
        {
            options.Mode = ZipArchiveMode.Read;
        });
    });
}
```

### Http response body deserialization

You can also specify the ZipArchiveMode if a specific HTTP endpoint differs from the default.

```csharp
public async Task<Post> GetAsync(int id)
{
    return await _client
        .GetAsync("posts")
        .WithQueryParameter("zip", true)
        .ExpectSuccess()
        // mode is optional
        .As<Post>(settings, mode: ZipArchiveMode.Read)
    ;
}
```
