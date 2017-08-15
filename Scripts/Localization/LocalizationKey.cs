using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public enum LocalizationKey
{
    GameTitle,
    Goal,
}

public static class LocalizationKeyExtension
{
    private static string[] names = {
        "game_title",
        "goal",
    };
    
    public static string GetKey(this LocalizationKey key)
    {
        return names[(int)key];
    }
}
