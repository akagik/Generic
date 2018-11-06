using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AndroidUtils
{
    public static string GetInternalStoragePath()
    {
        string path = "";
        using(AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            using(AndroidJavaObject currentActivity = jc.GetStatic<AndroidJavaObject>("currentActivity"))
            {
                path = currentActivity.Call<AndroidJavaObject>("getFilesDir").Call<string>("getCanonicalPath");
            }
        }
        return path;
    }
}
