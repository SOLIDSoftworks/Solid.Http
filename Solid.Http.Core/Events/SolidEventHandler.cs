using Solid.Http.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Solid.Http.Events
{
    internal class SolidEventHandler<T>
    {
        public Action<IServiceProvider, T> Noop => (_, __) => { };
        public Action<IServiceProvider, T> Handler { get; set; }
    }
}
