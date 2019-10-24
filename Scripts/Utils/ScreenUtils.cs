using UnityEngine;

public static class ScreenUtils
{
    public static float dpi
    {
        get
        {
#if UNITY_EDITOR
            // Unity Editor の場合は端末の大きさを仮定して DPI を計算する
            return 1080 / 2.64f;
#else
            return Screen.dpi;
#endif
        }
    }
}