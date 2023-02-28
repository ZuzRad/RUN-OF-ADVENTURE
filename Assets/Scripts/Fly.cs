using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly : MonoBehaviour
{
    [Header("Jump settings")]
    public Transform player;
    public float power;

    private Rigidbody2D rb;
    private Vector2 position;


    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.layer == 7)
        {
            position = new(0, power);
            rb = collision.gameObject.GetComponent<Rigidbody2D>();
            rb.AddForce(position, ForceMode2D.Impulse);
        }
    }
}
