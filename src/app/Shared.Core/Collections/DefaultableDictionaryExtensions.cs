using System.Collections.Generic;

namespace Shared.Core.Collections
{
    public static class DefaultableDictionaryExtensions
    {
        public static IDictionary<TKey, TValue> WithDefaultValue<TValue, TKey>(this IDictionary<TKey, TValue> dictionary, TValue defaultValue)
        {
            return new DefaultableDictionary<TKey, TValue>(dictionary, defaultValue);
        }
    }
}
