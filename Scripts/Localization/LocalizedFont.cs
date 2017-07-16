using UnityEngine;
using UnityEngine.UI;

public class LocalizedFont : MonoBehaviour
{
    public LocalizationFontType type;
    public int fontSize;

    public void Start()
    {
        Text text = GetComponent<Text>();

        text.font = LocalizationManager.Instance.GetFont(type);
        text.fontSize = LocalizationManager.Instance.CalcFontSize(fontSize, type);
    }

}
