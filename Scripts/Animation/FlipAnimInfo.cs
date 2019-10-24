using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

#endif

/// <summary>
/// 連番アニメーションを管理するクラス.
/// 連番アニメーションは単純に Sprite の配列と等価.
/// </summary>
public class FlipAnimInfo : ScriptableObject
{
    public Sprite[] sprites;

#if UNITY_EDITOR
    [MenuItem("Assets/Create/Generic/FlipAnimInfo")]
    public static void CreateInstance()
    {
        FlipAnimInfo obj = ScriptableObject.CreateInstance<FlipAnimInfo>();
        Generic.ScriptableObjectCreator.Create<FlipAnimInfo>(obj, name: "NewFlipAnimInfo");
    }
#endif
}