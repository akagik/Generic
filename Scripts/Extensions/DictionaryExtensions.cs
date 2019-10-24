using System.Collections.Generic;
using System.Linq;

public static class DictionaryExtensions
{
    public static V Get<K, V>(this Dictionary<K, V> self, K key, V defaultValue = default(V))
    {
        V value;
        return self.TryGetValue(key, out value) ? value : defaultValue;
    }

    public static string ToString<TKey, TValue>(this IDictionary<TKey, TValue> dictionary)
    {
        return "{" + string.Join(", ", dictionary.Select(kv => "'" + kv.Key + "': " + kv.Value).ToArray()) + "}";
    }
}