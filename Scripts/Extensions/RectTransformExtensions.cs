using UnityEngine;

public static class RectTransformExtensions
{
    public static void SetAnchoredPositionX(this RectTransform t, float x)
    {
        Vector2 pos = t.anchoredPosition;
        pos.x = x;
        t.anchoredPosition = pos;
    }

    public static void SetAnchoredPositionY(this RectTransform t, float y)
    {
        Vector2 pos = t.anchoredPosition;
        pos.y = y;
        t.anchoredPosition = pos;
    }

    public static void SetRight(this RectTransform t, float value)
    {
        Vector2 pos = t.offsetMax;
        pos.x = value;
        t.offsetMax = pos;
    }

    public static void SetTop(this RectTransform t, float value)
    {
        Vector2 pos = t.offsetMax;
        pos.y = value;
        t.offsetMax = pos;
    }

    public static void SetLeft(this RectTransform t, float value)
    {
        Vector2 pos = t.offsetMin;
        pos.x = value;
        t.offsetMin = pos;
    }

    public static void SetBottom(this RectTransform t, float value)
    {
        Vector2 pos = t.offsetMin;
        pos.y = value;
        t.offsetMin = pos;
    }
}