using UnityEngine;
using UnityEngine.UI;

public class FlipAnimImage : FlipAnimation {
    [SerializeField]
    Image image;

    public override void SetSprite(Sprite sprite) {
        image.sprite = sprite;
    }
}
