using System;
using System.Linq;
using Random = UnityEngine.Random;

public class WAM
{
    public float[] weights;
    public float wsm;
    public float[] p;
    public int[] a;

    public WAM(float[] weightArray)
    {
        weights = new float[weightArray.Length];
        Array.Copy(weightArray, 0, weights, 0, weightArray.Length);

        p = new float[weights.Length];
        a = new int[weights.Length];

        Setup();
    }

    private void Setup()
    {
        int[] hl = new int[weights.Length];
        int l = 0;
        int h = weights.Length - 1;

        a = new int[weights.Length];

        wsm = weights.Sum();
        Array.Clear(a, 0, weights.Length);
        Array.Clear(hl, 0, weights.Length);

        for (int i = 0; i < p.Length; i++)
        {
            p[i] = weights[i] * p.Length / wsm;
        }

        for (int i = 0; i < p.Length; i++)
        {
            if (p[i] < 1)
            {
                hl[l] = i;
                l += 1;
            }
            else
            {
                hl[h] = i;
                h -= 1;
            }
        }

        while (l > 0 && h < p.Length - 1)
        {
            int j = hl[l - 1];
            int k = hl[h + 1];

            a[j] = k;
            p[k] += p[j] - 1;

            if (p[k] < 1)
            {
                hl[l - 1] = k;
                h += 1;
            }
            else
            {
                l -= 1;
            }
        }
    }

    public int SelectOne()
    {
        float r = Random.Range(0f, 1f) * p.Length;
        int i = (int) Math.Floor(r);
        r -= i;

        return r < p[i] ? i : a[i];
    }
}