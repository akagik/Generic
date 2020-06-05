namespace Generic
{
#if UNITY_EDITOR
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEditor;

    /// <summary>
    /// Editor で使われるユーティリティクラス.
    /// </summary>
    public class EditorUtils
    {
        /// <summary>
        /// 指定のフィルターの唯一のアセットをロードする.
        /// アセットが見つからなかったり、複数見つかった場合はエラー.
        /// </summary>
        public static T LoadOnlyOneAsset<T>(string filter) where T : UnityEngine.Object
        {
            string guid = FindGUIDOfOnlyOneAsset(filter);
            if (guid == null)
            {
                return null;
            }

            string path = AssetDatabase.GUIDToAssetPath(guid);
            return AssetDatabase.LoadAssetAtPath<T>(path);
        }

        /// <summary>
        /// 指定のフィルターの唯一のアセットの GUID を検索して返す.
        /// アセットが見つからなかったり、複数見つかった場合はエラー.
        /// </summary>
        public static string FindGUIDOfOnlyOneAsset(string filter)
        {
            string[] guids = AssetDatabase.FindAssets(filter);

            if (guids.Length == 0)
            {
                Debug.LogErrorFormat("指定のフィルターのアセットが見つかりません", filter);
                return null;
            }

            if (guids.Length > 1)
            {
                Debug.LogErrorFormat("指定のフィルターのアセットが複数見つかりました", filter);
                return null;
            }

            return guids[0];
        }
    }
#endif
}