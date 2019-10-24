using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class AdaptingEventSystemDragThreshold : MonoBehaviour
{
    [SerializeField]
    private EventSystem eventSystem;

    [SerializeField]
    private float inchDistance = 0.08f;

    void Awake()
    {
        UpdatePixelDrag();
    }

    public void UpdatePixelDrag()
    {
        if (eventSystem == null)
        {
            Debug.LogWarning("Trying to set pixel drag for adapting to screen dpi, " +
                             "but there is no event system assigned to the script", this);
        }

        eventSystem.pixelDragThreshold = Mathf.RoundToInt(ScreenUtils.dpi * inchDistance);
    }
}