using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Character, IDamageable
{
    public float currentHealthEnemy = 15;
    [Header("Combat")]
    public Transform meleeAttackOrigin = null;
    public GameObject money = null;
    public float meleeAttackRadius = 0.6f;
    public float meleeAttackDelay = 1.1f;
    public bool enableMeleeAttack = false;
    public HPBar HPBar;

    [Header("Player object")]
    public LayerMask playerLayer = 7;
    public Transform player;

    private float timeUntilMeleeReadied = 0;
    private Vector3 lastPosition;
    private bool isMeleAttacking = false;
    private bool isDead = false;
    private float startTimeAttack = 3f;

    void Start()
    {
        Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        currentHealthEnemy = StaticBoss.bossMaxHealth;
        HPBar.SetHealth(currentHealthEnemy, StaticBoss.bossMaxHealth);
        lastPosition = transform.position;
    }
    void Update()
    {
        startTimeAttack -= Time.deltaTime;
        HandleMeleAttack();
        Rotation();
        RunAnimation();
    }

    private void RunAnimation()
    {
        Vector3 offset = transform.position - lastPosition;
        if (offset.x != 0)
        {
            lastPosition = transform.position;
            Animator.SetTrigger("isMoving");
        }
    }

    private void Rotation()
    {
        if (player != null)
        {
            var rotation = player.position - transform.position;
            if (rotation.x > 0)
                transform.rotation = Quaternion.Euler(0, 180f, 0);
            else
                transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    private IEnumerator MeleeAttackDelay()
    {
        Animator.SetTrigger("attack");
        isMeleAttacking = true;
        yield return new WaitForSeconds(meleeAttackDelay);
        isMeleAttacking = false;
    }

    private void HandleMeleAttack()
    {
        if (enableMeleeAttack && !isDead && startTimeAttack <= 0)
        {
            Collider2D[] overlappedColliders = Physics2D.OverlapCircleAll(meleeAttackOrigin.position, meleeAttackRadius, playerLayer);
            if (timeUntilMeleeReadied <= 0 && overlappedColliders.Length != 0)
            {
                timeUntilMeleeReadied = meleeAttackDelay;
                for (int i = 0; i < overlappedColliders.Length; i++)
                {
                    IDamageable enemyAttributes = overlappedColliders[i].GetComponent<IDamageable>();
                    if (enemyAttributes != null && !isMeleAttacking)
                    {
                        StartCoroutine(MeleeAttackDelay());
                        enemyAttributes.ApplyDamage(StaticBoss.bossMeleeDamage);
                    }
                }
            }
            else
                timeUntilMeleeReadied -= Time.deltaTime;
        }
    }

    protected IEnumerator OneMoneyWait()
    {
        for (int i = 0; i < 5; i++)
        {
            Instantiate(money, transform.position + new Vector3(-i/2f, -1.5f+i/2f, 0), transform.rotation);
            Instantiate(money, transform.position + new Vector3(i / 2f, -1.5f + i / 2f, 0), transform.rotation);
        }

        yield return new WaitForSeconds(0.5f);
        isDead = true;
        Die();
    }

    public virtual void ApplyDamage(float amount)
    {
        SoundManager.PlaySound(SoundManager.Sounds.Hit);
        currentHealthEnemy -= amount;
        HPBar.SetHealth(currentHealthEnemy, StaticBoss.bossMaxHealth);
        if (currentHealthEnemy <= 0)
            StartCoroutine(OneMoneyWait());
    }
}
