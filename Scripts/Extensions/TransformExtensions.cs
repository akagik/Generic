using UnityEngine;

public static class TransformExtensions
{
    /// <summary>
    /// この Transform の postion.x の値を変更する.
    /// </summary>
    public static void SetWorldX(this Transform t, float x)
    {
        Vector3 pos = t.position;
        pos.x = x;
        t.position = pos;
    }

    /// <summary>
    /// この Transform の postion.y の値を変更する.
    /// </summary>
    public static void SetWorldY(this Transform t, float y)
    {
        Vector3 pos = t.position;
        pos.y = y;
        t.position = pos;
    }

    /// <summary>
    /// この Transform の postion.z の値を変更する.
    /// </summary>
    public static void SetWorldZ(this Transform t, float z)
    {
        Vector3 pos = t.position;
        pos.z = z;
        t.position = pos;
    }

    /// <summary>
    /// この Transform の postion.x に x を加える.
    /// </summary>
    public static void AddWorldX(this Transform t, float x)
    {
        t.SetWorldX(t.position.x + x);
    }

    /// <summary>
    /// この Transform の postion.y に y を加える.
    /// </summary>
    public static void AddWorldY(this Transform t, float y)
    {
        t.SetWorldY(t.position.y + y);
    }

    /// <summary>
    /// この Transform の postion.z に z を加える.
    /// </summary>
    public static void AddWorldZ(this Transform t, float z)
    {
        t.SetWorldZ(t.position.z + z);
    }

    /// <summary>
    /// この Transform の localPostion.x の値を変更する.
    /// </summary>
    public static void SetLocalX(this Transform t, float x)
    {
        Vector3 pos = t.localPosition;
        pos.x = x;
        t.localPosition = pos;
    }

    /// <summary>
    /// この Transform の localPostion.y の値を変更する.
    /// </summary>
    public static void SetLocalY(this Transform t, float y)
    {
        Vector3 pos = t.localPosition;
        pos.y = y;
        t.localPosition = pos;
    }

    /// <summary>
    /// この Transform の localPostion.z の値を変更する.
    /// </summary>
    public static void SetLocalZ(this Transform t, float z)
    {
        Vector3 pos = t.localPosition;
        pos.z = z;


        t.localPosition = pos;
    }

    public static void SetLocalRotX(this Transform t, float x)
    {
        Vector3 rot = t.localRotation.eulerAngles;
        rot.x = x;

        Quaternion rotation = Quaternion.Slerp(t.localRotation, Quaternion.Euler(rot), 1);


        t.localRotation = rotation;
    }

    public static void SetLocalRotY(this Transform t, float y)
    {
        Vector3 rot = t.localRotation.eulerAngles;
        rot.y = y;

        Quaternion rotation = Quaternion.Slerp(t.localRotation, Quaternion.Euler(rot), 1);
        t.localRotation = rotation;
    }

    public static void SetLocalRotZ(this Transform t, float z)
    {
        Vector3 rot = t.localRotation.eulerAngles;
        rot.z = z;

        Quaternion rotation = Quaternion.Slerp(t.localRotation, Quaternion.Euler(rot), 1);
        t.localRotation = rotation;
    }

    public static void SetRotX(this Transform t, float x)
    {
        Vector3 rot = t.rotation.eulerAngles;
        rot.x = x;

        Quaternion rotation = Quaternion.Slerp(t.rotation, Quaternion.Euler(rot), 1);


        t.rotation = rotation;
    }

    public static void SetRotY(this Transform t, float y)
    {
        Vector3 rot = t.rotation.eulerAngles;
        rot.y = y;

        Quaternion rotation = Quaternion.Slerp(t.rotation, Quaternion.Euler(rot), 1);
        t.rotation = rotation;
    }

    public static void SetRotZ(this Transform t, float z)
    {
        Vector3 rot = t.rotation.eulerAngles;
        rot.z = z;

        Quaternion rotation = Quaternion.Slerp(t.rotation, Quaternion.Euler(rot), 1);
        t.rotation = rotation;
    }

    /// <summary>
    /// この Transform の localScale.x の値を変更する.
    /// </summary>
    public static void SetScaleX(this Transform t, float x)
    {
        Vector3 scale = t.localScale;
        scale.x = x;
        t.localScale = scale;
    }

    /// <summary>
    /// この Transform の localScale.y の値を変更する.
    /// </summary>
    public static void SetScaleY(this Transform t, float y)
    {
        Vector3 scale = t.localScale;
        scale.y = y;
        t.localScale = scale;
    }

    /// <summary>
    /// この Transform の localScale.z の値を変更する.
    /// </summary>
    public static void SetScaleZ(this Transform t, float z)
    {
        Vector3 scale = t.localScale;
        scale.z = z;
        t.localScale = scale;
    }

    public static void CopyTransform(this Transform t, Transform transformToCopy)
    {
        t.position = transformToCopy.position;
        t.rotation = transformToCopy.rotation;
        t.localScale = transformToCopy.localScale;
    }

    public static void DestroyAllChildren(this Transform t)
    {
        for (int i = 0; i < t.childCount; i++)
        {
            UnityEngine.GameObject.Destroy(t.GetChild(i).gameObject);
        }
    }

    public static Vector3 PositionBetween(this Transform t, Transform t2, float normalizedPosBetween)
    {
        Vector3 pos = Vector3.zero;
        Vector3 placementVector = t2.position - t.position;
        pos = t.position + (placementVector * normalizedPosBetween);

        return pos;
    }
}