﻿using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// Enable() を呼び出してからログの記録を開始する.
/// キャンバスを表示/非表示するのには Show() / Hide() を使う.
/// </summary>
public class LogDebugger : MonoBehaviour
{
    public int maxLength = 5000;

    [SerializeField]
    private Text m_textUI = null;

    [SerializeField]
    private Text minButtonText;

    [SerializeField]
    private RectTransform rect;

    private Canvas canvas;

    public void Start()
    {
        m_textUI.text = "";
        canvas = GetComponent<Canvas>();
    }

    public void Clear()
    {
        m_textUI.text = "";
    }

    public void OnMMButtonClick()
    {
        if (minButtonText.text == "最大化")
        {
            Maximize();
        }
        else
        {
            Minimize();
        }
    }

    public void Minimize()
    {
        minButtonText.text = "最大化";
        rect.anchorMin = new Vector2(1f, 1f);
        rect.anchorMax = new Vector2(1f, 1f);
        rect.pivot = new Vector2(1f, 1f);
        rect.sizeDelta = new Vector2(500f, 500f);
    }

    public void Maximize()
    {
        minButtonText.text = "最小化";
        rect.sizeDelta = new Vector2(0f, 0f);
        rect.anchorMin = new Vector2(0f, 0f);
        rect.anchorMax = new Vector2(1f, 1f);
        rect.pivot = new Vector2(0.5f, 0.5f);
    }

    public void Enable()
    {
        Application.logMessageReceived += OnLogMessage;
    }

    public void Denable()
    {
        Application.logMessageReceived -= OnLogMessage;
    }

    public void Show()
    {
        canvas.enabled = true;
    }

    public void Hide()
    {
        canvas.enabled = false;
    }

    private void OnLogMessage(string i_logText, string i_stackTrace, LogType i_type)
    {
        if (string.IsNullOrEmpty(i_logText))
        {
            return;
        }

        if (!string.IsNullOrEmpty(i_stackTrace))
        {
            //switch (i_type)
            //{
            //    case LogType.Error:
            //    case LogType.Assert:
            //    case LogType.Exception:
            //        break;
            //    default:
            //        break;
            //}
            i_logText += System.Environment.NewLine + i_stackTrace;
        }

        switch (i_type)
        {
            case LogType.Error:
            case LogType.Assert:
            case LogType.Exception:
                i_logText = string.Format("<color=red>{0}</color>", i_logText);
                break;
            case LogType.Warning:
                i_logText = string.Format("<color=yellow>{0}</color>", i_logText);
                break;
            default:
                break;
        }

        if (m_textUI.text.Length > maxLength)
        {
            m_textUI.text = i_logText + System.Environment.NewLine + m_textUI.text.Substring(0, maxLength);
        }
        else
        {
            m_textUI.text = i_logText + System.Environment.NewLine + m_textUI.text;
        }


    }
}
