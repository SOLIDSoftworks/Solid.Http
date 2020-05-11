using System;
using System.Collections.Generic;
using System.Text;

namespace System.Collections.Generic
{
    internal static class DictionaryExtensions
    {
        public static bool TryGetValue<T>(this IDictionary<string, object> dictionary, string key, out T value)
        {
            if(dictionary.TryGetValue(key, out var boxed) && boxed is T unboxed)
            {
                value = unboxed;
                return true;
            }

            value = default;
            return false;
        }
    }
}
