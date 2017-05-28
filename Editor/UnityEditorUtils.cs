using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

class UnityEditorUtils : EditorWindow
{
    /// <summary>
    /// ヒエラルキー上の LocalizationText コンポーネントまたは LocalizationFont コンポーネントが
    /// アタッチされてるゲームオブジェクトを検索し、それらのフォントやフォントサイズを参照して、Text コンポーネントの
    /// 値を書き換える。
    /// </summary>
    [MenuItem("Tools/Update Localization Text")]
    static void UpdateLocalizationText()
    {
        var existingManager = GameObject.FindObjectOfType(typeof(LocalizationManager)) as LocalizationManager;

        SystemLanguage useLang = SystemLanguage.English;

        if (existingManager != null)
        {
            useLang = existingManager.useLanguage;
        }

        var managerObj = new GameObject("TempLocalizationManager");
        var manager = managerObj.AddComponent<LocalizationManager>();

        manager.LoadLocalizedText(useLang);

        foreach (var aObj in GameObject.FindObjectsOfType(typeof(LocalizedText)))
        {
            LocalizedText lt = aObj as LocalizedText;
            LocalizationValue value = manager.GetLocalizedValue(lt.key);
            Debug.Log("change text = " + value.value);

            Text text = lt.GetComponent<Text>();
            text.text = value.value;
            text.font = value.font;
            text.fontSize = lt.CalcFontSize(value);
        }

        foreach (var aObj in GameObject.FindObjectsOfType(typeof(LocalizedFont)))
        {
            LocalizedFont lf = aObj as LocalizedFont;
            Debug.Log("change font name: " + lf.name);

            Text text = lf.GetComponent<Text>();
            text.font = manager.GetFont(lf.type);
            text.fontSize = manager.CalcFontSize(lf.fontSize, lf.type);
        }

        DestroyImmediate(managerObj);
    }

}
