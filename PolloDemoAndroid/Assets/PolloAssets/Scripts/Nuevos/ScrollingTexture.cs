using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingTexture : MonoBehaviour
{
    public float scrollX = 0.5f;
    public float scrollY = 0.5f;
    private Renderer ren = null;

    void Awake()
    {
        ren = GetComponent<Renderer>();
    }

    void Update()
    {
        float OffsetX = Time.time * scrollX;
        float OffsetY = Time.time * scrollY;
        ren.material.mainTextureOffset = new Vector2(OffsetX, OffsetY);

    }
}
