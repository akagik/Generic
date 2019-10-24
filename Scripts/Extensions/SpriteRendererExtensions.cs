using UnityEngine;
using UnityEngine.UI;

public static class SpriteRendererExtensions
{
    /// <summary>
    /// この SpriteRenderer の alpha を変更する
    /// </summary>
    public static void SetAlpha(this SpriteRenderer sr, float newAlpha)
    {
        Color c = sr.color;
        c.a = newAlpha;
        sr.color = c;
    }
}