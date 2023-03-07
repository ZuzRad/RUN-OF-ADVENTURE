using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushPlatform : MonoBehaviour
{
    public GameObject wall;
    public GameObject destroyWall;
    public bool playerTrigger = false;
    private void OnTriggerEnter2D(Collider2D collision)
    { 
        if (collision.gameObject.layer == 15 || (playerTrigger && collision.gameObject.layer == 7))
        {
            Destroy(wall);
            Destroy(destroyWall);
            AstarPath.active.Scan();
        }
    }
}
