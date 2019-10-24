using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Material のプロパティ値を変化させることによる連番アニメーション.
/// TODO uv をずらすアニメーションのほうがパフォーマンスが良さそう.
/// </summary>
public class FlipAnimMaterial : FlipAnimation
{
    [Header("Material 設定")]
    [SerializeField] Material sourceMaterial;

    [SerializeField] string texturePropertyName = "_MainTex";

    private Material copiedMaterial;

    public override void Setup()
    {
        base.Setup();
        CreateMaterial();
    }

    public override void Setup(Sprite[] sprites)
    {
        base.Setup(sprites);
        CreateMaterial();
    }

    public override void Setup(Sprite[] sprites, float secPerSpr)
    {
        base.Setup(sprites, secPerSpr);
        CreateMaterial();
    }

    void CreateMaterial()
    {
        if (copiedMaterial == null)
        {
            copiedMaterial = new Material(sourceMaterial);
            copiedMaterial.hideFlags = HideFlags.HideAndDontSave;

            var renderer = GetComponent<Renderer>();
            Material[] mats = renderer.materials;
            mats[0] = copiedMaterial;
            renderer.materials = mats;
        }
    }

    void OnDisable()
    {
        if (copiedMaterial != null)
        {
            DestroyImmediate(copiedMaterial);
        }
    }

    /// <summary>
    /// Sprite.texture をセットする必要がある.
    /// Sprite アトラスを使う場合は別途対応が必要.
    /// </summary>
    public override void SetSprite(Sprite sprite)
    {
        copiedMaterial.SetTexture(texturePropertyName, sprite.texture);
    }
}