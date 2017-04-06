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

    public static bool isTouched
    {
        get
        {
            return touchCount > 0;
        }
    }

    public static TouchPhase phase
    {
        get
        {
#if UNITY_EDITOR || UNITY_STANDALONE
            if (Input.GetMouseButtonDown(0)) { return TouchPhase.Began; }
            if (Input.GetMouseButtonUp(0)) { return TouchPhase.Ended; }
            return TouchPhase.Moved;
#else
            return Input.GetTouch(0).phase;
#endif
        }
    }

    public static Vector3 position
    {
        get
        {
#if UNITY_EDITOR || UNITY_STANDALONE
            return Input.mousePosition;
#else
            Touch touch = Input.GetTouch(0);
            TouchPosition.x = touch.position.x;
            TouchPosition.y = touch.position.y;
            return TouchPosition;
#endif
        }
    }
}