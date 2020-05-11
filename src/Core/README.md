# Solid.Http.Core
_Solid.Http.Core_ is where most of the included extension methods are located. It can be used on it's own if you don't need to serialize/deserialize content. The Http request/response is done using _HttpClient_ which is provided by the _IHttpClientFactory_ in _Microsoft.Extensions.Http_.

```cli
> dotnet add package Solid.Http.Core
```

## Initialization
You can do basic intialization, and _Solid.Http.Core_ will work just fine.

```csharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddSolidHttpCore();
}
```

## Global event handlers
You can also register event handlers during initialization.

```csharp
public void ConfigureServices(IServiceCollection services)
{
    services
        .AddSolidHttpCore(builder => 
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

## Important interfaces
_Solid.Http.Core_ includes some interfaces that you will, well, interface with.

### ISolidHttpClientFactory
_ISolidHttpClientFactory_ is where you request an _ISolidHttpClient_ so that you can perform http request fluently and asynchronously. The interface has two methods.

#### ISolidHttpClientFactory.Create
_Create()_ does what it says. It creates an _ISolidHttpClient_ and provides it to you. This client expects a full and absolute url when it is performing an HTTP request.

#### ISolidHttpClientFactory.CreateWithBaseAddress
_CreateWithBaseAddress(Uri baseAddress)_ creates an _ISolidHttpClient_ that has a base address. Requests made using this client should be relative.

##### About relative addresses (a bit of a tangent)
Some people don't realize the difference between __/posts__ and __posts__ as relative addresses. 

__/posts__ will go to the root of the baseAddress that it's used on. So if you have a base address of _https://mysite.com/api/_, a relative address of __/posts__ would point to _https://mysite.com/posts_, while __posts__ would point to _https://mysite.com/api/posts_.

### ISolidHttpClient
_ISolidHttpClient_ is what you use to create an _ISolidHttpRequest_, which is the interface that has the most extension methods for the fluent syntax.

#### ISolidHttpClient.BaseAddress
_BaseAddress_ is a readonly property that is populated when you create the client using _ISolidHttpClientFactory.CreateWithBaseAddress_.

#### ISolidHttpClient.OnRequestCreated
_OnRequestCreated_ is where the event handlers are registered for when an _ISolidHttpRequest_ is created. They will be run along with any event handlers that were registered when _Solid.Http.Core_ was added to the _IServiceCollection_.

#### ISolidHttpClient.PerformRequestAsync
_PerformRequestAsync_ is what creates an _ISolidHttpRequest_ and starts the fluent syntax of the http request. This method is what the request method extension methods call internally, i.e. GetAsync, PostAsync etc.

### ISolidHttpRequest
_ISolidHttpRequest_ is an awaitable interface which performs an http request and returns an HttpResponseMessage.

#### ISolidHttpRequest.Services
This is the root _IServiceProvider_ and is passed to the event handlers that are run on the _ISolidHttpRequest_. It can also be used in extension methods on _ISolidHttpRequest_.

#### ISolidHttpRequest.Context
The is a simple hashtable of items that can be used in extension methods if you need context between multiple methods.

#### ISolidHttpRequest.CancellationToken
The cancellation token to be used by _HttpClient_.

#### ISolidHttpRequest.BaseRequest
This is the _HttpRequestMessage_ that will be passed to _HttpClient_ and sent over the wire.

#### ISolidHttpRequest.BaseResponse
This will be populated once the _ISolidHttpRequest_ has been awaited.

#### ISolidHttpRequest.OnHttpRequest
_OnHttpRequest_ is where the event handlers are registered that run just before the http request is sent. They will be run along with any event handlers that were registered when _Solid.Http.Core_ was added to the _IServiceCollection_.

#### ISolidHttpRequest.OnHttpResponse
_OnHttpResponse_ is where the event handlers are registered that are run directly after a response has be received. They will be run along with any event handlers that were registered when _Solid.Http.Core_ was added to the _IServiceCollection_.

#### ISolidHttpRequest.As
_As_ is a method for deserialization of the response content body.

#### ISolidHttpRequest.GetAwaiter
_GetAwaiter_ is what enables the ISolidHttpRequest to be awaited. Once the _ISolidHttpRequest_ is awaited, it will return a _HttpResponseMessage_.