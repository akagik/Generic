using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// セーブで使われるシリアライザー/ディシリアライザーのインターフェース.
/// </summary>
public interface ISaveSerializer
{
    byte[] Serialize<T>(T target);
    T Deserialize<T>(byte[] bytes);
}