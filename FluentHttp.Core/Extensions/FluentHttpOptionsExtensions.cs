using System;
namespace FluentHttp
{
    public static class FluentHttpOptionsExtensions
    {
        public static IFluentHttpOptions AddOptions(this IFluentHttpOptions options, Action<IFluentHttpOptions> configure)
        {
            configure(options);
            return options;
        }
    }
}
