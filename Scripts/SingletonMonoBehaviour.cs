﻿using System;
using UnityEngine;

public abstract class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                Type t = typeof(T);

                instance = (T)FindObjectOfType(t);
                if (instance == null)
                {
                    Debug.LogErrorFormat("{0} をアタッチしているGameObjectはありません", t);
                }
            }

            return instance;
        }
    }

    virtual protected void Awake()
    {
        if (this != Instance)
        {
            Destroy(gameObject);

            Debug.LogErrorFormat("{0} は既に他のGameObjectにアタッチされているため、コンポーネントを破棄しました. アタッチされているGameObjectは {1} です.", typeof(T), Instance.gameObject.name);

            return;
        }

        // シーンを跨いでこのオブジェクトが消されないようにする
        DontDestroyOnLoad(this.gameObject);
    }
}
