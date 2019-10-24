using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// JsonUtility によるシリアライズ・ディシリアライズ.
/// </summary>
public class JsonSaveSerializer : ISaveSerializer
{
    public byte[] Serialize<T>(T target)
    {
        return System.Text.Encoding.UTF8.GetBytes(JsonUtility.ToJson(target));
    }

    public T Deserialize<T>(byte[] ivBytes)
    {
        string json = System.Text.Encoding.UTF8.GetString(ivBytes);
        return JsonUtility.FromJson<T>(json);
    }
}