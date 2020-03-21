using System;
using UnityEngine;

/// <summary>
/// 連番アニメーションを再生する.
/// Image, SpriteRenderer の両方に対応できるように抽象クラスとして定義.
/// </summary>
public abstract class FlipAnimation : MonoBehaviour
{
    [SerializeField]
    float secPerSpr;

    [SerializeField]
    Sprite[] sprites;

    float elappsedSeconds;
    int _currentIndex;
    bool _isStop;
    private bool playOnce;

    public bool playOnAwake;
    public bool autoUpdate;
    public Action onComplete;

    public int currentIndex => _currentIndex;
    public bool isStop => _isStop;

    void Awake()
    {
        if (playOnAwake)
        {
            Play();
        }
    }

    public virtual void Setup()
    {
        // do nothing
    }

    public virtual void Setup(Sprite[] sprites)
    {
        this.sprites = sprites;
    }

    public virtual void Setup(Sprite[] sprites, float secPerSpr)
    {
        this.sprites = sprites;
        this.secPerSpr = secPerSpr;
    }

    public void Stop()
    {
        _isStop = true;
    }

    public void Kill(bool complete = true)
    {
        Stop();

        if (complete)
        {
            var _onComplete = onComplete;
            onComplete = null;
            _onComplete?.Invoke();
        }
    }

    public void Play()
    {
        playOnce = false;
        _isStop = false;
    }

    public void PlayFromStart()
    {
        elappsedSeconds = 0;
        _currentIndex = 0;
        _isStop = false;
        playOnce = false;
    }
    
    public void PlayOnce()
    {
        PlayFromStart();
        playOnce = true;
    }

    void Update()
    {
        if (autoUpdate)
        {
            OnUpdate();
        }
    }

    // アニメーションが1周したときは True を返す.
    public bool OnUpdate()
    {
        if (_isStop)
        {
            return false;
        }

        SetSprite(sprites[_currentIndex]);
        elappsedSeconds += Time.deltaTime;
        if (elappsedSeconds >= secPerSpr)
        {
            elappsedSeconds = 0f;
            _currentIndex = (_currentIndex + 1) % sprites.Length;
            bool isEnd = _currentIndex == 0;

            if (isEnd && playOnce)
            {
                Kill(true);
            } 

            return isEnd;
        }

        return false;
    }

    /// <summary>
    /// Sprite をセットする.
    /// SpriteRenderer、Image、Material などに対応できるように
    /// メソッドを abstract にしている.
    /// </summary>
    public abstract void SetSprite(Sprite sprite);
}