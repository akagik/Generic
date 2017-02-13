using System.Collections.Generic;

public static class DictionaryExtensions
{
    public static V Get<K, V>(this Dictionary<K, V> self, K key, V defaultValue = default(V))
    {
        V value;
        return self.TryGetValue(key, out value) ? value : defaultValue;
    }
}
