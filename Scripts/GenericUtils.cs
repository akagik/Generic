using UnityEngine;


public static class GenericUtils
{
    public static Vector3 RandomVector3(float min, float max)
    {
        float x = UnityEngine.Random.Range(min, max);
        float y = UnityEngine.Random.Range(min, max);
        float z = UnityEngine.Random.Range(min, max);

        return new Vector3(x, y, z);
    }
}