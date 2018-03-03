using UnityEngine;
using System;

public static class GenericUtils
{
    public static Vector3 RandomVector3(float min, float max)
    {
        float x = UnityEngine.Random.Range(min, max);
        float y = UnityEngine.Random.Range(min, max);
        float z = UnityEngine.Random.Range(min, max);

        return new Vector3(x, y, z);
    }

    public static Vector2 RandomVector2(float min, float max)
    {
        float x = UnityEngine.Random.Range(min, max);
        float y = UnityEngine.Random.Range(min, max);

        return new Vector2(x, y);
    }

    public static void Swap<T>(ref T lhs, ref T rhs)
    {
        T temp;
        temp = lhs;
        lhs = rhs;
        rhs = temp;
    }

    public static void InsertSort<T>(T[] a) where T : IComparable<T>
    {
        int n = a.Length;
        for (int i = 1; i < n; i++)
            for (int j = i; j >= 1; --j)
            {
                if (a[j - 1] != null && a[j - 1].CompareTo(a[j]) <= 0)
                {
                    break;
                }
                else if (a[j] != null && a[j].CompareTo(a[j - 1]) > 0)
                {
                    break;
                }
                GenericUtils.Swap(ref a[j], ref a[j - 1]);
            }
    }

    public static void InsertSort<T>(T[] a, Func<T, IComparable> key)
    {
        int n = a.Length;
        for (int i = 1; i < n; i++)
            for (int j = i; j >= 1; --j)
            {
                if (a[j - 1] != null && key(a[j - 1]).CompareTo(key(a[j])) <= 0)
                {
                    break;
                }
                else if (a[j] != null && key(a[j]).CompareTo(key(a[j - 1])) > 0)
                {
                    break;
                }
                GenericUtils.Swap(ref a[j], ref a[j - 1]);
            }
    }

}