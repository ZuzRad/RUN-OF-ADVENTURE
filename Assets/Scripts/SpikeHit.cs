using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeHit : MonoBehaviour
{
    public float damage = 1f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable playerAttributes = collision.GetComponent<IDamageable>();
        if (collision.gameObject.layer == 7 && playerAttributes != null)
            playerAttributes.ApplyDamage(damage);
    }
}
