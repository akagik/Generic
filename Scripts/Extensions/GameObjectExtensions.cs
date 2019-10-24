using UnityEngine;

public static class GameObjectExtensions
{
    public static T GetRequiredComponent<T>(this GameObject obj) where T : Object
    {
        T component = obj.GetComponent<T>();

        if (component == null)
        {
            Debug.LogErrorFormat("Expected to find component of type {0} but found none", typeof(T), obj);
        }

        return component;
    }

    /// <summary>
    /// 自分自身を含むすべての子オブジェクトのレイヤーを設定します
    /// </summary>
    public static void SetLayerRecursively(this GameObject self, int layer)
    {
        self.layer = layer;

        foreach (Transform n in self.transform)
        {
            SetLayerRecursively(n.gameObject, layer);
        }
    }
}