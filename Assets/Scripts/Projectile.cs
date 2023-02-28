using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public Rigidbody2D rb = null;
    public float speed = 15f;
    public float delaySeconds = 3f;

    private WaitForSeconds cullDelay = null;

    void Start()
    {
        cullDelay = new WaitForSeconds(delaySeconds);
        StartCoroutine(DelayedCull());
        rb.velocity = transform.right * speed;
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            IDamageable enemyAttributes=collision.GetComponent<IDamageable>();
            if(enemyAttributes != null)
            {
                enemyAttributes.ApplyDamage(StaticPlayerStats.rangeDamage);
            }

            gameObject.SetActive(false);
            Destroy(gameObject);
        }

        if (collision.gameObject.layer == 10)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }

    private IEnumerator DelayedCull()
    {
        yield return cullDelay;
        gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
