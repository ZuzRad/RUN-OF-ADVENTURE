using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_enemy : MonoBehaviour
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
        if (collision.gameObject.layer == 7)
        {
            IDamageable playerAttributes = collision.GetComponent<IDamageable>();
            if (playerAttributes != null)
                playerAttributes.ApplyDamage(StaticEnemyStats.enemyRangeDamage);

            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else if (collision.gameObject.layer == 10)
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
