using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RainbowUIEffect : BaseMeshEffect
{
    [SerializeField]
    public float startHue = 0f;

    [SerializeField]
    [Range(0f, 360f)]
    float angle = 0f;

    [SerializeField]
    [Range(1, 128)]
    int dividingLength = 6;

    private Color[] _colors;

    private Color[] colors
    {
        get
        {
            if (_colors != null)
            {
                if (dividingLength + 1 != _colors.Length)
                {
                    createColor();
                }
                else
                {
                    float h, s, v;
                    Color.RGBToHSV(_colors[0], out h, out s, out v);

                    if (!float.Equals(h, startHue))
                    {
                        createColor();
                    }
                }
            }
            else
            {
                createColor();
            }

            return _colors;
        }
    }

    private void createColor()
    {
        float dh = 1f / dividingLength;
        _colors = new Color[dividingLength + 1];


        float start = startHue;
        float m = Mathf.Floor(start);

        if (start > 0f)
        {
            start = start - m;
        }
        else
        {
            start = (start - m);
        }

        for (int i = 0; i < _colors.Length; i++)
        {
            float newHue = start + dh * i;
            newHue = (newHue > 1f) ? newHue - 1 : newHue;
            _colors[i] = Color.HSVToRGB(newHue, 1f, 1f);
        }
    }

    public void SetVerticesDirty()
    {
        var graphics = base.GetComponent<Graphic>();
        if (graphics != null)
        {
            graphics.SetVerticesDirty();
        }
    }

    public override void ModifyMesh(VertexHelper vh)
    {
        var list = UIVerticesPool.Get();
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

        float[] fs = new float[dividingLength + 1];
        for (int i = 0; i < fs.Length; i++)
        {
            fs[i] = (max - min) * i / dividingLength;
        }

        for (int i = 0, count = list.Count; i < count; ++i)
        {
            var vertex = list[i];

            var dot = vertex.position.x * vector.x + vertex.position.y * vector.y;

            int index = getIndex(dot, fs);

            var t = Mathf.InverseLerp(fs[index], fs[index + 1], dot);
            vertex.color = Color32.Lerp(colors[index], colors[index + 1], t);
            list[i] = vertex;
        }

        vh.Clear();
        vh.AddUIVertexTriangleStream(list);
    }

    private int getIndex(float dot, float[] fx)
    {
        for (int i = 0; i < fx.Length - 1; i++)
        {
            if (dot <= fx[i + 1])
            {
                return i;
            }
        }

        Debug.LogErrorFormat("Error: dot: {0}", dot);
        return 0;
    }
}