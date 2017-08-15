[System.Serializable]
public enum LocalizationKey
{
}

public static class LocalizationKeyExtension
{
    private static string[] names = {
    };
    
    public static string GetKey(this LocalizationKey key)
    {
        return names[(int)key];
    }
}
