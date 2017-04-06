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

}