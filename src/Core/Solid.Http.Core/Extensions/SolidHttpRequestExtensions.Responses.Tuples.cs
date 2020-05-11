using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Solid.Http
{
    /// <summary>
    /// Extension methods for deserializing <see cref="HttpContent" /> to c# tuples.
    /// </summary>
    public static class Solid_Http_SolidHttpRequestExtensions_Responses_Tuples
    {
        /// <summary>
        /// Returns a tuple that includes one property that represents a successful response of type <typeparamref name="TSuccess" />
        /// and another property that represents an unsuccessful response of type <typeparamref name="TError" />.
        /// </summary>
        /// <typeparam name="TSuccess">The type that represents a successful response.</typeparam>
        /// <typeparam name="TError">The type that represents an error response.</typeparam>
        /// <param name="request">The <see cref="ISolidHttpRequest" /> that is being extended.</param>
        /// <returns>
        /// A <see cref="ValueTask{T}" /> of type <seealso cref="Tuple{T1, T2}" />.
        /// <para>The first type parameter in the <see cref="Tuple{T1, T2}" /> is <typeparamref name="TSuccess" />.await</para>
        /// <para>The second type parameter in the <see cref="Tuple{T1, T2}" /> is <typeparamref name="TError" />.await</para>
        /// </returns>
        public static ValueTask<(TSuccess Success, TError Error)> As<TSuccess, TError>(this ISolidHttpRequest request)
            => request.As<TSuccess, TError>(response => response.IsSuccessStatusCode);

        /// <summary>
        /// Returns a tuple that includes one property that represents a response that passes a <paramref name="tPredicate" /> and deserializes into 
        /// type <typeparamref name="T" /> and another property that represents the fallback response of type <typeparamref name="TFallback" />.
        /// </summary>
        /// <typeparam name="T">The type that that represents when <paramref name="tPredicate" /> is true.</typeparam>
        /// <typeparam name="TFallback">The type that represents the fallback response.</typeparam>
        /// <param name="request">The <see cref="ISolidHttpRequest" /> that is being extended.</param>
        /// <param name="tPredicate">A predicate that represents a response that is supposed to deserialize as <typeparamref name="T" />.</param>
        /// <returns>
        /// A <see cref="ValueTask{T}" /> of type <seealso cref="Tuple{T1, T2}" />.
        /// <para>The first type parameter in the <see cref="Tuple{T1, T2}" /> is <typeparamref name="T" />.await</para>
        /// <para>The second type parameter in the <see cref="Tuple{T1, T2}" /> is <typeparamref name="TFallback" />.await</para>
        /// </returns>
        public static async ValueTask<(T, TFallback)> As<T, TFallback>(
            this ISolidHttpRequest request,
            Func<HttpResponseMessage, bool> tPredicate
        )
        {
            var provider = null as DeserializerProvider;
            var response = await request.OnHttpResponse((services, _) => provider = services.GetRequiredService<DeserializerProvider>());
            var t1 = default(T);
            var t2 = default(TFallback);
            if (tPredicate(response))
                t1 = await response.Content.ReadAsAsync<T>(provider);
            else 
                t2 = await response.Content.ReadAsAsync<TFallback>(provider);
            return (t1, t2);
        }

        /// <summary>
        /// Returns a tuple that includes one property that represents a response that passes a <paramref name="t1Predicate" /> and deserializes into 
        /// type <typeparamref name="T1" />, another property that represents a response that passes a <paramref name="t2Predicate" /> and deserializes
        /// into <typeparamref name="T2" />, and a third property that represents the fallback response of type <typeparamref name="TFallback" />.
        /// </summary>
        /// <typeparam name="T1">The type that that represents when <paramref name="t1Predicate" /> is true.</typeparam>
        /// <typeparam name="T2">The type that that represents when <paramref name="t2Predicate" /> is true.</typeparam>
        /// <typeparam name="TFallback">The type that represents the fallback response.</typeparam>
        /// <param name="request">The <see cref="ISolidHttpRequest" /> that is being extended.</param>
        /// <param name="t1Predicate">A predicate that represents a response that is supposed to deserialize as <typeparamref name="T1" />.</param>
        /// <param name="t2Predicate">A predicate that represents a response that is supposed to deserialize as <typeparamref name="T2" />.</param>
        /// <returns>
        /// A <see cref="ValueTask{T}" /> of type <seealso cref="Tuple{T1, T2, T3}" />.
        /// <para>The first type parameter in the <see cref="Tuple{T1, T2, T3}" /> is <typeparamref name="T1" />.</para>
        /// <para>The second type parameter in the <see cref="Tuple{T1, T2, T3}" /> is <typeparamref name="T2" />.</para>
        /// <para>The third type parameter in the <see cref="Tuple{T1, T2, T3}" /> is <typeparamref name="TFallback" />.</para>
        /// </returns>
        public static async ValueTask<(T1, T2, TFallback)> As<T1, T2, TFallback>(
            this ISolidHttpRequest request,
            Func<HttpResponseMessage, bool> t1Predicate,
            Func<HttpResponseMessage, bool> t2Predicate
        )
        {
            var provider = null as DeserializerProvider;
            var response = await request.OnHttpResponse((services, _) => provider = services.GetRequiredService<DeserializerProvider>());
            var t1 = default(T1);
            var t2 = default(T2);
            var t3 = default(TFallback);
            if (t1Predicate(response))
                t1 = await response.Content.ReadAsAsync<T1>(provider);
            else if (t2Predicate(response))
                t2 = await response.Content.ReadAsAsync<T2>(provider);
            else
                t3 = await response.Content.ReadAsAsync<TFallback>(provider);
            return (t1, t2, t3);
        }

        /// <summary>
        /// Returns a tuple that includes one property that represents a response that passes a <paramref name="t1Predicate" /> and deserializes into 
        /// type <typeparamref name="T1" />, another property that represents a response that passes a <paramref name="t2Predicate" /> and deserializes
        /// into <typeparamref name="T2" />, a third property that represents a response that passes a <paramref name="t3Predicate" /> and deserializes
        /// into <typeparamref name="T3" />, and a fourth property that represents the fallback response of type <typeparamref name="TFallback" />.
        /// </summary>
        /// <typeparam name="T1">The type that that represents when <paramref name="t1Predicate" /> is true.</typeparam>
        /// <typeparam name="T2">The type that that represents when <paramref name="t2Predicate" /> is true.</typeparam>
        /// <typeparam name="T3">The type that that represents when <paramref name="t3Predicate" /> is true.</typeparam>
        /// <typeparam name="TFallback">The type that represents the fallback response.</typeparam>
        /// <param name="request">The <see cref="ISolidHttpRequest" /> that is being extended.</param>
        /// <param name="t1Predicate">A predicate that represents a response that is supposed to deserialize as <typeparamref name="T1" />.</param>
        /// <param name="t2Predicate">A predicate that represents a response that is supposed to deserialize as <typeparamref name="T2" />.</param>
        /// <param name="t3Predicate">A predicate that represents a response that is supposed to deserialize as <typeparamref name="T2" />.</param>
        /// <returns>
        /// /// A <see cref="ValueTask{T}" /> of type <seealso cref="Tuple{T1, T2, T3, T4}" />.
        /// <para>The first type parameter in the <see cref="Tuple{T1, T2, T3, T4}" /> is <typeparamref name="T1" />.</para>
        /// <para>The second type parameter in the <see cref="Tuple{T1, T2, T3, T4}" /> is <typeparamref name="T2" />.</para>
        /// <para>The third type parameter in the <see cref="Tuple{T1, T2, T3, T4}" /> is <typeparamref name="T2" />.</para>
        /// <para>The fourth type parameter in the <see cref="Tuple{T1, T2, T3, T4}" /> is <typeparamref name="TFallback" />.</para>
        /// </returns>
        public static async ValueTask<(T1, T2, T3, TFallback)> As<T1, T2, T3, TFallback>(
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
            var t4 = default(TFallback);
            if (t1Predicate(response))
                t1 = await response.Content.ReadAsAsync<T1>(provider);
            else if (t2Predicate(response))
                t2 = await response.Content.ReadAsAsync<T2>(provider);
            else if (t3Predicate(response))
                t3 = await response.Content.ReadAsAsync<T3>(provider);
            else
                t4 = await response.Content.ReadAsAsync<TFallback>(provider);
            return (t1, t2, t3, t4);
        }
    }
}
