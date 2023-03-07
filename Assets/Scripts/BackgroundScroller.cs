using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    [Range(-1f, 1f)]
    public float scrollSpeed = 1f;
    public Renderer backgroundRenderer;
    private float offset;

    private void Update()
    {
        offset = (Time.deltaTime + scrollSpeed) / 1000f;
        backgroundRenderer.material.mainTextureOffset +=new Vector2(offset, 0f);
    }
}
