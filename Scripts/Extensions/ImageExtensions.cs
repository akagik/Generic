using UnityEngine;
using UnityEngine.UI;

public static class ImageExtensions
{
    /// <summary>
    /// この Text の alpha を変更する
    /// </summary>
    public static void SetAlpha(this Image image, float newAlpha)
    {
        Color c = image.color;
        c.a = newAlpha;
        image.color = c;
    }

    public static void SetColorExcludingAlpha(this Image image, Color color)
    {
        Color c = image.color;
        c.r = color.r;
        c.g = color.g;
        c.b = color.b;
        image.color = c;
    }
}