using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Solid.Http.Events
{
    internal class SolidAsyncEventHandler<T>
    {
        public Func<IServiceProvider, T, Task> Noop => (_, __) => Task.CompletedTask;
        public Func<IServiceProvider, T, Task> Handler { get; set; }
    }
}
