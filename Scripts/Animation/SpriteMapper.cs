using UnityEngine;
using System.Collections.Generic;

public class SpriteMapper : MonoBehaviour
{
    [SerializeField] SpriteRenderer sr;
    [SerializeField] SpriteMap map;

    public void LateUpdate()
    {
        sr.sprite = map.GetSprite(sr.sprite);
    }
}