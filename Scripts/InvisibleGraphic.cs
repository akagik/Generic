using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;

#endif

public class InvisibleGraphic : Graphic
{
    protected override void OnPopulateMesh(VertexHelper vh)
    {
        base.OnPopulateMesh(vh);
        vh.Clear();
    }

#if UNITY_EDITOR

    [CustomEditor(typeof(InvisibleGraphic))]
    class GraphicCastEditor : Editor
    {
        public override void OnInspectorGUI()
        {
        }
    }

#endif
}