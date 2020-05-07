using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace System
{
    internal static class FuncExtensions
    {
        public static async ValueTask InvokeAllAsync<T>(this Func<IServiceProvider, T, ValueTask> handler, IServiceProvider services, T t)
        {
            if (handler == null) return;

            var list = handler.GetInvocationList().Cast<Func<IServiceProvider, T, ValueTask>>();
            
            foreach (var func in list)
                await func(services, t);
        }

        public static Func<IServiceProvider, T, ValueTask> Add<T>(this Func<IServiceProvider, T, ValueTask> handler, Func<IServiceProvider, T, ValueTask> next)
        {
            if (next == null) return handler;
            if (handler == null) return next;
            return (Func<IServiceProvider, T, ValueTask>)(Delegate.Combine(handler, next));
        }

        public static Func<IServiceProvider, T, ValueTask> Add<T>(this Func<IServiceProvider, T, ValueTask> handler, Func<T, ValueTask> next)
            => handler.Add((_, t) => next(t));

        public static Func<IServiceProvider, T, ValueTask> Add<T>(this Func<IServiceProvider, T, ValueTask> handler, Action<IServiceProvider, T> next)
            => handler.Add((services, t) =>
            {
                next(services, t);
                return new ValueTask();
            });

        public static Func<IServiceProvider, T, ValueTask> Add<T>(this Func<IServiceProvider, T, ValueTask> handler, Action<T> next)
            => handler.Add((_, t) => next(t));

        public static Func<IServiceProvider, T, ValueTask> Convert<T>(this Func<T, ValueTask> handler)
            => (_, t) => handler(t);

        public static Func<IServiceProvider, T, ValueTask> Convert<T>(this Action<IServiceProvider, T> handler)
            => (services, t) =>
             {
                 handler(services, t);
                 return new ValueTask();
             };

        public static Func<IServiceProvider, T, ValueTask> Convert<T>(this Action<T> handler)
            => (_, t) =>
            {
                handler(t);
                return new ValueTask();
            };
    }
}
