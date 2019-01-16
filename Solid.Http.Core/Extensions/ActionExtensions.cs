using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Solid.Http.Extensions
{
    internal static class ActionExtensions
    {
        public static Func<IServiceProvider, T, Task> ToAsyncFunc<T>(this Action<IServiceProvider, T> action)
        {
            return new Func<IServiceProvider, T, Task>((services, obj) =>
            {
                action(services, obj);
                return Task.CompletedTask;
            });
        }
    }
}
