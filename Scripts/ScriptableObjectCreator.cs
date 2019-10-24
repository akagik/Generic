using System.IO;
#if UNITY_EDITOR
using UnityEditor;

namespace Generic
{
    public static class ScriptableObjectCreator
    {
        public static void Create<T>(T t, string name = "NewAsset") where T : UnityEngine.Object
        {
            string path = AssetDatabase.GetAssetPath(Selection.activeObject);

            if (path == "")
            {
                path = "Assets";
            }
            else if (Path.GetExtension(path) != "")
            {
                path = path.Replace(Path.GetFileName(path), "");
            }

            string filePath = Path.Combine(path, name + ".asset");
            AssetDatabase.CreateAsset(t, AssetDatabase.GenerateUniqueAssetPath(filePath));
            AssetDatabase.Refresh();
        }
    }
}
#endif