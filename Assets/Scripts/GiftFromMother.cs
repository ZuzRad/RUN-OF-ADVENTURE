using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiftFromMother : MonoBehaviour
{
    public GameObject gift = null;
    public Transform player;
    private bool spawn = false;
    private bool temp = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.layer == 7 && spawn == false)
        {

           Instantiate(gift, transform.position + new Vector3(1, 0.2f, 0), transform.rotation);
            spawn = true;

        }



    }

    private void Update()
    {
        if (!player && spawn && !temp)
        {
            StaticPlayerStats.maxHealth -= 5f;
            temp = true;
        }
    }
}
