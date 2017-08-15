[System.Serializable]
public class LocalizationData
{
    public LocalizationItem[] items;

    public LocalizationItem GetItemAt(string key)
    {
        foreach(LocalizationItem item in items)
        {
            if (item.key == key)
            {
                return item;
            }
        }
        return null;
    }

    public bool ContainsKey(string key)
    {
        foreach (LocalizationItem item in items)
        {
            if (item.key == key && item.value.Trim().Length > 0)
            {
                return true;
            }
        }
        return false;
    }
}

[System.Serializable]
public class LocalizationItem
{
    public string key;
    public string value;
    public string type;
}
