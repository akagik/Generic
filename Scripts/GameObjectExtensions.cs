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
}
