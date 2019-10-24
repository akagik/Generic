using UnityEngine;
using UnityEngine.UI;

public static class TextExtensions
{
    /// <summary>
    /// この Text の alpha を変更する
    /// </summary>
    public static void SetAlpha(this Text text, float newAlpha)
    {
        Color c = text.color;
        c.a = newAlpha;
        text.color = c;
    }
}