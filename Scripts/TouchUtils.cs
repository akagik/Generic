using UnityEngine;


public static class TouchUtils
{
    public static int touchCount
    {
        get
        {
#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBGL
            if (Input.GetMouseButtonDown(0)) return 1;
            if (Input.GetMouseButton(0)) return 1;
            if (Input.GetMouseButtonUp(0)) return 1;
            return 0;
#else
            return Input.touchCount;
#endif
        }
    }

#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBGL
    static int updateFrameCount;
    static Vector2 previousPosition;
    static Vector2 currentPosition;
    static float previousTime;
    static float currentTime;
#endif

    public static Touch GetTouch(int index)
    {
#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBGL
        Touch touch = new Touch();
        touch.position = Input.mousePosition;

        if (updateFrameCount != Time.frameCount)
        {
            previousPosition = currentPosition;
            previousTime = currentTime;
            
            currentPosition = touch.position;
            currentTime = Time.time;

            updateFrameCount = Time.frameCount;
        }

        touch.deltaPosition = touch.position - previousPosition;
        touch.deltaTime = currentTime - previousTime;

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