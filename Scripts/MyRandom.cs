using UnityEngine;
using Random = UnityEngine.Random;

public static class MyRandom
{
    public static float NextGaussian(float mu = 0, float sigma = 1)
    {
        var u1 = Random.value;
        var u2 = Random.value;

        var rand_std_normal = Mathf.Sqrt(-2.0f * Mathf.Log(u1)) * Mathf.Sin(2.0f * Mathf.PI * u2);
        var rand_normal = mu + sigma * rand_std_normal;

        return rand_normal;
    }

    public static float NextExp(float lambda = 1f)
    {
        var u = Random.value;

        return -Mathf.Log(1 - u);
    }
}