using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;


public static class HashSetExtensions
{
    public static T First<T>(this HashSet<T> set)
    {
        foreach (T t in set)
        {
            return t;
        }

        throw new Exception("set must have at least one element.");
    }

    public static T Choice<T>(this HashSet<T> set)
    {
        int i = 0;
        int target = Random.Range(0, set.Count);

        foreach (T t in set)
        {
            if (i == target)
            {
                return t;
            }

            i++;
        }

        throw new Exception("Index is out of bound.");
    }
}