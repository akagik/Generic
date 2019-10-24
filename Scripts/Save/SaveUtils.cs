using System;
using System.IO;

/// <summary>
/// セーブ・ロード周りに使われるユーティリティクラス.
/// </summary>
public static class SaveUtils
{
    /// <summary>
    /// 指定のセーブデータが存在する場合は true を返す.
    /// </summary>
    public static bool ExistsSaveData(string filePath)
    {
        return File.Exists(filePath);
    }

    /// <summary>
    /// 指定のディシリアライザーを用いて指定のファイルを読み込んでディシリアライズする.
    /// </summary>
    public static T Load<T>(string filePath, Func<byte[], T> deserializer)
    {
        byte[] bytes;
        ReadAllBytes(filePath, out bytes);
        return deserializer(bytes);
    }

    /// <summary>
    /// 指定のシリアライザーを用いて指定のファイルにデータを書き込みする.
    /// </summary>
    public static void Save<T>(string filePath, T data, Func<T, byte[]> serializer)
    {
        byte[] bytes = serializer(data);
        WriteAllBytes(filePath, bytes);
    }

    public static bool WriteAllBytes(string filePath, byte[] bytes)
    {
        File.WriteAllBytes(filePath, bytes);
        return true;
    }

    public static bool ReadAllBytes(string filePath, out byte[] bytes)
    {
        bytes = File.ReadAllBytes(filePath);
        return true;
    }
}