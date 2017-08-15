using UnityEngine;
using UnityEngine.UI;

public class LocalizedText : MonoBehaviour
{
    public LocalizationKey key;
    public int fontSize;

    public int CalcFontSize(LocalizationValue value)
    {
        return (int)(fontSize * value.fontSizeScale);
    }

    public void Start()
    {
        Text text = GetComponent<Text>();

        LocalizationValue value = LocalizationManager.Instance.GetLocalizedValue(key.GetKey());

        text.text = value.value;
        text.font = value.font;

        text.fontSize = CalcFontSize(value);
    }

}
