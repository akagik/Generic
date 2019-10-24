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
    bool isStop;

    public bool playOnAwake;
    public bool autoUpdate;

    public int currentIndex
    {
        get { return _currentIndex; }
    }

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
        isStop = true;
    }

    public void Play()
    {
        isStop = false;
    }

    public void PlayFromStart()
    {
        elappsedSeconds = 0;
        _currentIndex = 0;
        isStop = false;
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
        if (isStop)
        {
            return false;
        }

        SetSprite(sprites[_currentIndex]);
        elappsedSeconds += Time.deltaTime;
        if (elappsedSeconds >= secPerSpr)
        {
            elappsedSeconds = 0f;
            _currentIndex = (_currentIndex + 1) % sprites.Length;
            return _currentIndex == 0;
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