using UnityEngine;


public static class TouchUtils
{
    private static Vector3 TouchPosition = Vector3.zero;

    public static int touchCount
    {
        get
        {
#if UNITY_EDITOR || UNITY_STANDALONE
            if (Input.GetMouseButtonDown(0)) return 1;
            if (Input.GetMouseButton(0)) return 1;
            if (Input.GetMouseButtonUp(0)) return 1;
            return 0;
#else
            return Input.touchCount;
#endif
        }
    }

    public static Touch GetTouch(int index)
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        Touch touch = new Touch();
        touch.position = Input.mousePosition;

        if (Input.GetMouseButtonDown(0)) touch.phase = TouchPhase.Began;
        else if (Input.GetMouseButtonUp(0)) touch.phase = TouchPhase.Ended;
        else touch.phase = TouchPhase.Moved;

        touch.fingerId = 0;

        return touch;
#else
            return Input.GetTouch(index);
#endif
    }
}