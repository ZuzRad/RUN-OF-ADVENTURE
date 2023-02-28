using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossArena : MonoBehaviour
{
    public GameObject wall1;
    public GameObject wall2;
    public GameObject wall3;
    public GameObject wall4;



    public GameObject wall5;

    private bool isActive = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.layer == 7 && !isActive)
        {
            Instantiate(wall1);
            Instantiate(wall2);

            Instantiate(wall3);
            Instantiate(wall4);

            Destroy(wall5);
            isActive = true;
            AstarPath.active.Scan();
        }


    }
}
