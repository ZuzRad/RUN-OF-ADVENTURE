using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeHit : MonoBehaviour
{
    public float damage = 1f;
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.layer == 7)
        {
            IDamageable playerAttributes = collision.GetComponent<IDamageable>();
            if (playerAttributes != null)
            {
                playerAttributes.ApplyDamage(damage);
            }
        }


    }
}
