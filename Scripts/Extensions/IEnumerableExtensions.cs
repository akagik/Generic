using System.Collections.Generic;

public static class IEnumerableExtensions
{
    public static void ForEach<T>(this IEnumerable<T> source, System.Action<T> action)
    {
        foreach (T obj in source)
            action(obj);
    }
}