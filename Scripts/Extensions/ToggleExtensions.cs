using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class ToggleExtensions
{
    static Toggle.ToggleEvent emptyToggleEvent = new Toggle.ToggleEvent();

    /// <summary>
    /// イベントトリガーなしで isOn の値を変更する.
    /// </summary>
    public static void SetOnWithoutEvent(this Toggle toggle, bool isOn)
    {
        var originalEvent = toggle.onValueChanged;
        toggle.onValueChanged = emptyToggleEvent;
        toggle.isOn = isOn;
        toggle.onValueChanged = originalEvent;
    }
}