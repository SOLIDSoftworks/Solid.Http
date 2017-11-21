using SolidHttp.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace SolidHttp.Setup
{
    internal class SolidHttpInitializer : ISolidHttpInitializer
    {
        private bool _initialized;
        private ISolidHttpOptions _options;
        private IEnumerable<Action<ISolidHttpOptions>> _actions;

        public SolidHttpInitializer(ISolidHttpOptions options, IEnumerable<Action<ISolidHttpOptions>> actions)
        {
            _options = options;
            _actions = actions;
        }

        public bool IsInitialized => _initialized;

        public void Initialize()
        {
            if (_initialized) return;

            foreach (var action in _actions)
                action(_options);
            _initialized = true;
        }
    }
}
