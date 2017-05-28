using UnityEngine;


public static class TouchUtils
{
    private static Vector3 TouchPosition = Vector3.zero;

    public static int touchCount
    {
        get
        {
#if UNITY_ANDROID || UNITY_IPHONE
            return Input.touchCount;
#else
            if (Input.GetMouseButtonDown(0)) return 1;
            if (Input.GetMouseButton(0)) return 1;
            if (Input.GetMouseButtonUp(0)) return 1;

            return 0;
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
#if UNITY_ANDROID || UNITY_IPHONE
            return Input.GetTouch(0).phase;
#else
            if (Input.GetMouseButtonDown(0)) { return TouchPhase.Began; }
            if (Input.GetMouseButtonUp(0)) { return TouchPhase.Ended; }
            return TouchPhase.Moved;
#endif
        }
    }

    public static Vector3 position
    {
        get
        {
#if UNITY_ANDROID || UNITY_IPHONE
            Touch touch = Input.GetTouch(0);
            TouchPosition.x = touch.position.x;
            TouchPosition.y = touch.position.y;
            return TouchPosition;
#else
            return Input.mousePosition;
#endif
        }
    }
}