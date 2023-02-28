using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    [Range(-1f, 1f)]
    public float scrollSpeed = 1f;
    public Renderer backgroundRenderer;
    private float offset;
    //private Material mat;
    //void Start()
    //{
    //    var tr = GetComponent<Renderer>();
    //    tr.sortingLayerName = "Forward";
    //    mat = GetComponent<Renderer>().material;
    //}

    //void Update()
    //{
    //    offset += (Time.deltaTime + scrollSpeed) / 10f;
    //    mat.SetTextureOffset("_MainTex", new Vector2(offset, 0));
    //}

    private void Update()
    {
        offset = (Time.deltaTime + scrollSpeed) / 1000f;
        backgroundRenderer.material.mainTextureOffset +=new Vector2(offset, 0f);
    }
}
