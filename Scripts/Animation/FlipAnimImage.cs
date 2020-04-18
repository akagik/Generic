using UnityEngine;
using UnityEngine.UI;

public class FlipAnimImage : FlipAnimation
{
    [SerializeField]
    Image image;

    public Image targetImage => image;

    public override void SetSprite(Sprite sprite)
    {
        image.sprite = sprite;
    }
}