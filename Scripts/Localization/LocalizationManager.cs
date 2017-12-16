using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

/// <summary>
/// 言語ごとにテキストを局所化するために機能を備えたクラス。
/// 
/// 言語ごとに設定できるのは、文字列、フォント、フォントサイズスケール。
/// LocalizationManager はテキストの種類として Header と Content の２つを持ち、
/// 種類ごとのフォントを設定出来る。
/// 
/// このクラスは使用している言語を Application.systemLanguage を参照して言語を判断するが、
/// enforce フラグを True にすることで指定の言語に強制することができる。
/// また、Application.systemLanguage の言語のリソースが存在しない場合はデフォルトの言語を使う。
/// </summary>
public class LocalizationManager : SingletonMonoBehaviour<LocalizationManager>
{
    public bool isReady { get; private set; }

    public SystemLanguage DefaultLang = SystemLanguage.English;

    public bool enforce;
    public SystemLanguage enforceLanguage;

    [SerializeField]
    private List<ScaleSettings> scaleSettings = new List<ScaleSettings>();

    public SystemLanguage useLanguage
    {
        get
        {
            if (enforce)
            {
                return enforceLanguage;
            }
            else
            {
                return Application.systemLanguage;
            }
        }
    }

    [ReadOnly]
    public SystemLanguage usingLanguage;

    [ReadOnly]
    [SerializeField]
    private Font defaultHeaderFont;

    [ReadOnly]
    [SerializeField]
    private Font defaultContentFont;

    // 端末言語ヘッダー→端末言語コンテンツ→デフォルト言語ヘッダーの順番で検索して
    // 存在するものを利用する
    [ReadOnly]
    [SerializeField]
    private Font _headerFont;
    private Font headerFont
    {
        get
        {
            if (_headerFont != null)
            {
                return _headerFont;
            }
            else if (_contentFont != null)
            {
                return _contentFont;
            }
            return defaultHeaderFont;
        }
    }

    [ReadOnly]
    [SerializeField]
    private Font _contentFont;
    private Font contentFont
    {
        get
        {
            if (_contentFont != null)
            {
                return _contentFont;
            }
            else if (_headerFont != null)
            {
                return _headerFont;
            }
            return defaultContentFont;
        }
    }

    private Dictionary<string, LocalizationValue> localizedText;

    private string missingTextString = "Localized text not found";

    [System.Serializable]
    private class LocalizationData
    {
        public LocalizationItem[] items;

        public LocalizationItem GetItemAt(string key)
        {
            foreach (LocalizationItem item in items)
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
    private class LocalizationItem
    {
        public string key = null;
        public string value = null;
        public string type = null;
    }

    public int CalcFontSize(int fontBaseSize, LocalizationFontType type)
    {
        checkReady();

        return (int)(fontBaseSize * getScale(usingLanguage, type));
    }

    public static string GetCode(SystemLanguage lang)
    {
        switch (lang)
        {
            case SystemLanguage.Japanese:
                return "ja";
            case SystemLanguage.Korean:
                return "ko";
            case SystemLanguage.Chinese:
            case SystemLanguage.ChineseSimplified:
            case SystemLanguage.ChineseTraditional:
                return "zh";
            case SystemLanguage.English:
            default:
                return "en";
        }
    }

    public Font GetFont(LocalizationFontType type)
    {
        checkReady();

        if (type == LocalizationFontType.Header)
        {
            return headerFont;
        }
        return contentFont;
    }

    public void LoadLocalizedText(SystemLanguage lang)
    {
        this.usingLanguage = lang;

        // フォントデータを読み込む
        defaultHeaderFont = GetLocalizationFont(DefaultLang, LocalizationFontType.Header);
        defaultContentFont = GetLocalizationFont(DefaultLang, LocalizationFontType.Content);

        _headerFont = GetLocalizationFont(lang, LocalizationFontType.Header);
        _contentFont = GetLocalizationFont(lang, LocalizationFontType.Content);

        localizedText = new Dictionary<string, LocalizationValue>();

        // 指定された lang とデフォルトの言語のテキストデータを取得する
        LocalizationData loadedData = GetLocalizationData(lang);
        LocalizationData defaultLoadedData = GetLocalizationData(DefaultLang);

        foreach (LocalizationKey key in Enum.GetValues(typeof(LocalizationKey)))
        {
            string keyStr = key.GetKey();

            LocalizationValue value;

            if (loadedData.ContainsKey(keyStr))
            {
                LocalizationItem item = loadedData.GetItemAt(keyStr);

                LocalizationFontType fontType = (LocalizationFontType)Enum.Parse(typeof(LocalizationFontType), item.type, true);

                if (fontType == LocalizationFontType.Header)
                {
                    value = new LocalizationValue(item.value, headerFont, getScale(lang, fontType));
                }
                else
                {
                    value = new LocalizationValue(item.value, contentFont, getScale(lang, fontType));
                }
            }
            else if (defaultLoadedData.ContainsKey(key.GetKey()))
            {
                LocalizationItem item = defaultLoadedData.GetItemAt(keyStr);

                LocalizationFontType fontType = (LocalizationFontType)Enum.Parse(typeof(LocalizationFontType), item.type, true);

                if (fontType == LocalizationFontType.Header)
                {
                    value = new LocalizationValue(item.value, headerFont);
                }
                else
                {
                    value = new LocalizationValue(item.value, contentFont);
                }
            }
            else
            {
                value = new LocalizationValue(missingTextString, defaultHeaderFont);
            }

            localizedText.Add(keyStr, value);
        }

        isReady = true;
        Debug.Log("Data loaded, dictionary contains: " + localizedText.Count + " entries : " + lang);
    }

    private LocalizationData GetLocalizationData(SystemLanguage lang)
    {
        LocalizationData loadedData;

        string filePath = Path.Combine(Path.Combine("Localization", GetCode(lang)), "strings");
        TextAsset targetFile = Resources.Load<TextAsset>(filePath);

        if (targetFile == null)
        {
            loadedData = new LocalizationData();
            loadedData.items = new LocalizationItem[0];
        }
        else
        {
            loadedData = JsonUtility.FromJson<LocalizationData>(targetFile.text);
        }
        return loadedData;
    }

    public Font GetLocalizationFont(SystemLanguage lang, LocalizationFontType type)
    {
        string filename = string.Format("{0}Font", type.ToString());
        string filePath = Path.Combine(Path.Combine("Localization", GetCode(lang)), filename);

        Font font = Resources.Load<Font>(filePath);

        return font;
    }

    public bool Contains(string key)
    {
        return localizedText.ContainsKey(key);
    }

    public string Get(LocalizationKey key)
    {
        LocalizationValue value = GetLocalizedValue(key.GetKey());
        return value.value;
    }

    public string Get(string key)
    {
        LocalizationValue value = GetLocalizedValue(key);
        return value.value;
    }

    public LocalizationValue GetLocalizedValue(string key)
    {
        checkReady();

        LocalizationValue result = new LocalizationValue(missingTextString, defaultHeaderFont);

        if (localizedText.ContainsKey(key))
        {
            result = localizedText[key];
        }

        return result;
    }

    public LocalizationValue GetLocalizedValue(LocalizationKey key)
    {
        return GetLocalizedValue(key.GetKey());
    }

    private float getScale(SystemLanguage lang, LocalizationFontType type)
    {
        foreach (ScaleSettings settings in scaleSettings)
        {
            if (settings.lang == lang && settings.type == type)
            {
                return settings.scale;
            }
        }
        return 1f;
    }

    private void checkReady()
    {
        if (!isReady)
        {
            isReady = true;

            LoadLocalizedText(useLanguage);
        }
    }
}

public struct LocalizationValue
{
    public string value;
    public Font font;
    public float fontSizeScale;

    public LocalizationValue(string _value, Font _font, float scale = 1f)
    {
        value = _value;
        font = _font;
        fontSizeScale = scale;
    }
}

public enum LocalizationFontType
{
    Header,
    Content,
}

[Serializable]
struct ScaleSettings
{
    public SystemLanguage lang;
    public LocalizationFontType type;
    public float scale;
}
