using UnityEngine;
using UnityEngine.UI;

public class FlipAnimSprRenderer : FlipAnimation
{
    [SerializeField]
    SpriteRenderer sprRenderer;

    public override void SetSprite(Sprite sprite)
    {
        sprRenderer.sprite = sprite;
    }

    public SpriteRenderer spriteRenderer => sprRenderer;
}