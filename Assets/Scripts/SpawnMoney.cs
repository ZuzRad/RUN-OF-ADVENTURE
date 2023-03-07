using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMoney : MonoBehaviour
{
    public float amount = 3f;
    public GameObject money = null;
    private bool spawn = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7 && spawn == false)
        {
            for (int i = 0; i < amount; i++)
                Instantiate(money, transform.position + new Vector3(i - 1, 0.4f, 0), transform.rotation);

            spawn = true;
        }
    }
}
