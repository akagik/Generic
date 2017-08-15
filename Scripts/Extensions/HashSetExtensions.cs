using System;
using System.Collections.Generic;

public static class HashSetExtensions
{
    public static T First<T>(this HashSet<T> set) {
        foreach (T t in set)
        {
            return t;
        }
        throw new Exception("set must have at least one element.");
    }
}
