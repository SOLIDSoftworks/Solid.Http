using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Solid.Http
{
    public static class Solid_Http_SolidHttpRequestExtensions_Responses_Tuples
    {
        public static ValueTask<(TSuccess Success, TError Error)> As<TSuccess, TError>(this ISolidHttpRequest request)
            => request.As<TSuccess, TError>(response => response.IsSuccessStatusCode);

        public static async ValueTask<(T, TDefault)> As<T, TDefault>(
            this ISolidHttpRequest request,
            Func<HttpResponseMessage, bool> tPredicate
        )
        {
            var provider = null as DeserializerProvider;
            var response = await request.OnHttpResponse((services, _) => provider = services.GetRequiredService<DeserializerProvider>());
            var t1 = default(T);
            var t2 = default(TDefault);
            if (tPredicate(response))
                t1 = await response.Content.ReadAsAsync<T>(provider);
            else 
                t2 = await response.Content.ReadAsAsync<TDefault>(provider);
            return (t1, t2);
        }

        public static async ValueTask<(T1, T2, TDefault)> As<T1, T2, TDefault>(
            this ISolidHttpRequest request,
            Func<HttpResponseMessage, bool> t1Predicate,
            Func<HttpResponseMessage, bool> t2Predicate
        )
        {
            var provider = null as DeserializerProvider;
            var response = await request.OnHttpResponse((services, _) => provider = services.GetRequiredService<DeserializerProvider>());
            var t1 = default(T1);
            var t2 = default(T2);
            var t3 = default(TDefault);
            if (t1Predicate(response))
                t1 = await response.Content.ReadAsAsync<T1>(provider);
            else if (t2Predicate(response))
                t2 = await response.Content.ReadAsAsync<T2>(provider);
            else
                t3 = await response.Content.ReadAsAsync<TDefault>(provider);
            return (t1, t2, t3);
        }

        public static async ValueTask<(T1, T2, T3, TDefault)> As<T1, T2, T3, TDefault>(
            this ISolidHttpRequest request,
            Func<HttpResponseMessage, bool> t1Predicate,
            Func<HttpResponseMessage, bool> t2Predicate,
            Func<HttpResponseMessage, bool> t3Predicate
        )
        {
            var provider = null as DeserializerProvider;
            var response = await request.OnHttpResponse((services, _) => provider = services.GetRequiredService<DeserializerProvider>());
            var t1 = default(T1);
            var t2 = default(T2);
            var t3 = default(T3);
            var t4 = default(TDefault);
            if (t1Predicate(response))
                t1 = await response.Content.ReadAsAsync<T1>(provider);
            else if (t2Predicate(response))
                t2 = await response.Content.ReadAsAsync<T2>(provider);
            else if (t3Predicate(response))
                t3 = await response.Content.ReadAsAsync<T3>(provider);
            else
                t4 = await response.Content.ReadAsAsync<TDefault>(provider);
            return (t1, t2, t3, t4);
        }
    }
}
