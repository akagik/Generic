using UnityEngine;
using UnityEngine.UI;

public abstract class LocalizedText : MonoBehaviour
{
    public abstract string GetKey();

    public int fontSize;

    public int CalcFontSize(LocalizationValue value)
    {
        return (int)(fontSize * value.fontSizeScale);
    }

    public void Start()
    {
        Text text = GetComponent<Text>();

        LocalizationValue value = LocalizationManager.Instance.GetLocalizedValue(GetKey());

        text.text = value.value;
        text.font = value.font;

        text.fontSize = CalcFontSize(value);
    }

}
