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
    int currentIndex;
    bool isStop;

    public bool playOnAwake;
    public bool autoUpdate;

    void Awake()
    {
        if(playOnAwake)
        {
            Play();
        }
    }

    public void Setup(Sprite[] sprites,float secPerSpr)
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
        currentIndex = 0;
        isStop = false;
    }

    void Update()
    {
        if(autoUpdate)
        {
            OnUpdate();
        }
    }

    // アニメーションが1周したときは True を返す.
    public bool OnUpdate()
    {
        if(isStop)
        {
            return false;
        }

        SetSprite(sprites[currentIndex]);
        elappsedSeconds += Time.deltaTime;
        if(elappsedSeconds >= secPerSpr)
        {
            elappsedSeconds = 0f;
            currentIndex = (currentIndex + 1) % sprites.Length;
            return true;
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
