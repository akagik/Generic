using UnityEngine;
using System.Collections.Generic;

public static class UIVerticesPool
{
    static System.WeakReference verticesBuffer;

    public static List<UIVertex> Get()
    {
        List<UIVertex> result = null;

        if (verticesBuffer != null)
        {
            result = verticesBuffer.Target as List<UIVertex>;
        }

        if (result == null)
        {
            result = new List<UIVertex>();
            verticesBuffer = new System.WeakReference(result);
        }

        return result;
    }
}