using UnityEngine;
using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;

public static class GenericUtils
{
    static Regex SnakeCaseRegex = new Regex("[a-z][A-Z]");

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

    public static void InsertSort<T>(List<T> a) where T : IComparable<T>
    {
        int n = a.Count;
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

                var temp = a[j];
                a[j] = a[j - 1];
                a[j - 1] = temp;
            }
    }

    /// <summary>
    /// 参照のコピーでなく実体のコピーを行う
    /// </summary>
    public static T DeepClone<T>(T obj)
    {
        using (var ms = new MemoryStream())
        {
            var formatter = new BinaryFormatter();
            formatter.Serialize(ms, obj);
            ms.Position = 0;

            return (T) formatter.Deserialize(ms);
        }
    }

    /// <summary>
    /// 四捨五入した値を返す.
    /// </summary>
    public static int MidRound(float fVal)
    {
        return (int) Math.Round(fVal, MidpointRounding.AwayFromZero);
    }

    public static string ToSnakeCase(string str)
    {
        var snakeStr = SnakeCaseRegex.Replace(str, m => m.Groups[0].Value[0] + "_" + m.Groups[0].Value[1]).ToLower();
        return snakeStr;
    }

    /// <summary>
    /// UTC 時間を JST 時間に変換する.
    /// </summary>
    public static DateTime ToJstTime(DateTime utc)
    {
        // Mac では Tokyo Standard Time が見つからないと怒られる
        // TimeZoneInfo tzi = TimeZoneInfo.FindSystemTimeZoneById("Tokyo Standard Time");
        // return TimeZoneInfo.ConvertTimeFromUtc(utc,tzi);

        // JST は UTC+0900
        // TODO これだとダメ！！！ロケールが UTC のままになっている
        return utc + TimeSpan.FromHours(9);
    }

    /// <summary>
    /// mod of negative
    /// </summary>
    public static int mod(int x, int m)
    {
        return (x % m + m) % m;
    }

    public static float fmod(float a, float b)
    {
        int n = (int) (a / b);
        a -= n * b;
        if (a < 0)
        {
            a += b;
        }

        return a;
    }

    public static Vector2 ToCanvasPosition(Vector3 worldPos, Camera camera, RectTransform canvas)
    {
        Vector2 ViewportPosition = camera.WorldToViewportPoint(worldPos);
        Vector2 WorldObject_ScreenPosition = new Vector2(
            ((ViewportPosition.x * canvas.sizeDelta.x) - (canvas.sizeDelta.x * 0.5f)),
            ((ViewportPosition.y * canvas.sizeDelta.y) - (canvas.sizeDelta.y * 0.5f)));
        return WorldObject_ScreenPosition;
    }
}