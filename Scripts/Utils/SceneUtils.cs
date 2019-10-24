using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// シーンに関連したユーティリティクラス.
/// </summary>
public static class SceneUtils
{
    /// <summary>
    /// 指定のシーンが読み込まれている場合は true を返す.
    /// </summary>
    public static bool IsSceneLoaded(string sceneName)
    {
        for (int i = 0; i < UnityEngine.SceneManagement.SceneManager.sceneCount; i++)
        {
            if (SceneManager.GetSceneAt(i).name == sceneName)
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// 指定のシーンを読み込んで、指定の型の参照を取得してそれを返す.
    /// 指定の型が見つかれば即座にそれを返す.
    /// </summary>
    public static IEnumerator WaitForSceneLoaded<T>(string sceneName) where T : UnityEngine.Object
    {
        T[] foundStates = GameObject.FindObjectsOfType<T>();

        // 型T のオブジェクトが見つかればそれをセットする.
        if (foundStates.Length > 0)
        {
            yield return foundStates[0];
            yield break;
        }

        // 型T のオブジェクトが見つからなければシーンをロードする.
        var op = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        while (!op.isDone)
        {
            yield return null;
        }

        // currentState が取得できるまで繰り返す.
        while (foundStates.Length == 0)
        {
            foundStates = GameObject.FindObjectsOfType<T>();
        }

        if (foundStates.Length > 1)
        {
            Debug.LogError("WaitForSceneLoaded: 対象の型のオブジェクトが複数個見つかりました");
        }

        yield return foundStates[0];
    }

    /// <summary>
    /// 指定の buildIndex のシーンを読み込んで、指定の型の参照を取得してそれを返す.
    /// 指定の型が見つかれば即座にそれを返す.
    /// </summary>
    public static IEnumerator WaitForSceneLoaded<T>(int buildIndex) where T : UnityEngine.Object
    {
        T[] foundStates = GameObject.FindObjectsOfType<T>();

        // 型T のオブジェクトが見つかればそれをセットする.
        if (foundStates.Length > 0)
        {
            yield return foundStates[0];
            yield break;
        }

        // 型T のオブジェクトが見つからなければシーンをロードする.
        var op = SceneManager.LoadSceneAsync(buildIndex, LoadSceneMode.Additive);
        while (!op.isDone)
        {
            yield return null;
        }

        // currentState が取得できるまで繰り返す.
        while (foundStates.Length == 0)
        {
            foundStates = GameObject.FindObjectsOfType<T>();
        }

        yield return foundStates[0];
    }

    /// <summary>
    /// シーンパスからシーン名のみを取り出して、それを返す.
    /// 
    ///　例えば "Assets/Scenes/01_Home.unity" というシーンパスの場合,
    /// "01_Home" を返す.
    /// </summary>
    public static string GetSceneName(string scenePath)
    {
        string[] pathSplits = scenePath.Split('/');
        string[] splits = pathSplits[pathSplits.Length - 1].Split('.');
        return splits[0];
    }

    /// <summary>
    /// 指定した buildIndex に対応するシーン名を返す.
    /// </summary>
    public static string GetSceneName(int buildIndex)
    {
        return SceneUtils.GetSceneName(SceneUtility.GetScenePathByBuildIndex(buildIndex));
    }
}