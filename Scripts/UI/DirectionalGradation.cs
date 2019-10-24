using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DirectionalGradation : BaseMeshEffect
{
    [SerializeField]
    Color32 color = Color.white;

    [SerializeField]
    [Range(0f, 360f)]
    float angle = 0f;

    public override void ModifyMesh(VertexHelper vh)
    {
        var list = new List<UIVertex>();
        vh.GetUIVertexStream(list);

        float max = float.MinValue;
        float min = float.MaxValue;

        var vector = new Vector2(Mathf.Cos(Mathf.Deg2Rad * angle), Mathf.Sin(Mathf.Deg2Rad * angle));
        foreach (var it in list)
        {
            var dot = it.position.x * vector.x + it.position.y * vector.y;
            max = Mathf.Max(dot, max);
            min = Mathf.Min(dot, min);
        }

        if (max == min)
        {
            return;
        }

        for (int i = 0, count = list.Count; i < count; ++i)
        {
            var vertex = list[i];

            var dot = vertex.position.x * vector.x + vertex.position.y * vector.y;
            var t = Mathf.InverseLerp(min, max, dot);
            vertex.color = Color32.Lerp(vertex.color, color, t);
            list[i] = vertex;
        }

        vh.Clear();
        vh.AddUIVertexTriangleStream(list);
    }
}