using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character, IDamageable
{
    public float currentHealthEnemy = 15;
    [Header("Combat")]
    public Transform meleeAttackOrigin = null;
    public Transform rangeAttackOrigin = null;
    public GameObject projectile = null;
    public GameObject money = null;
    public float meleeAttackRadius = 0.6f;
    public float meleeAttackDelay = 1.1f;
    public float rangeAttackDelay = 0.3f;
    public bool enableMeleeAttack = false;
    public bool enableRangeAttack = false;
    public HPBar HPBar;
    public bool newEnemy = true;

    [Header("Player object")]
    public LayerMask playerLayer = 7;
    public Transform player;

    private float timeUntilMeleeReadied = 0;
    private float timeUntilRangeReadied = 0;
    private Vector3 lastPosition;
    private bool isMeleAttacking = false;
    private bool isDead = false;
    private float startTimeAttack = 3f;
   // private float enemyStartTime = 3f;
    void Start()
    {
        Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        currentHealthEnemy= StaticEnemyStats.enemyMaxHealth;
        HPBar.SetHealth(currentHealthEnemy, StaticEnemyStats.enemyMaxHealth);
        lastPosition =transform.position;
    }
    void Update()
    {
        startTimeAttack -= Time.deltaTime;
        HandleMeleAttack();
        HandleRangeAtack();
        Rotation();
        RunAnimation();
        
    }

    private void HandleRangeAtack()
    {
        if (enableRangeAttack && !isDead && startTimeAttack<=0)  
        {
                if (timeUntilRangeReadied <= 0)
                {
                    if (newEnemy)
                    {
                    Animator.SetBool("Ability", true);
                    }

                Instantiate(projectile, rangeAttackOrigin.position, rangeAttackOrigin.rotation);

                    timeUntilRangeReadied = rangeAttackDelay;
                }
                else
                {
                    timeUntilRangeReadied -= Time.deltaTime;
                }
        }
    }
    private void RunAnimation()
    {
        Vector3 offset = transform.position - lastPosition;
        if (offset.x > 0)
        {
            lastPosition = transform.position;
            if (newEnemy)
            {
                Animator.SetBool("Run", false);
            }
            else
            {
                Animator.SetInteger("AnimState", 2);
            }
            
        }
        else if (offset.x < 0)
        {
            lastPosition = transform.position;
            if (newEnemy)
            {
                Animator.SetBool("Run", false);
            }
            else
            {
                Animator.SetInteger("AnimState", 2);
            }
        }

    }

    private void Rotation()
    {
        if(player != null)
        {
            var rotation = player.position - transform.position;

            if (rotation.x > 0)
            {

                transform.rotation = Quaternion.Euler(0, 180f, 0);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
        
    }


    private IEnumerator MeleeAttackDelay()
    {
        Animator.SetTrigger("Attack");
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
                for (int i = 0; i < overlappedColliders.Length; i++)
                {

                    IDamageable enemyAttributes = overlappedColliders[i].GetComponent<IDamageable>();
                    if (enemyAttributes != null)
                    {
                        if (!isMeleAttacking)
                        {
                            StartCoroutine(MeleeAttackDelay());
                            enemyAttributes.ApplyDamage(StaticEnemyStats.enemyMeleeDamage);
                        }


                    }
                }

                timeUntilMeleeReadied = meleeAttackDelay;
            }
            else
            {
                timeUntilMeleeReadied -= Time.deltaTime;
            }
        }
       
    }
    private void Money()
    {
        if(newEnemy)
            Instantiate(money, transform.position+new Vector3(0,-0.5f,0), transform.rotation);
        else
            Instantiate(money, transform.position + new Vector3(0, 0.4f, 0), transform.rotation);
    }
    protected IEnumerator OneMoneyWait()
    {
        Animator.SetTrigger("Death");
        Money();
        yield return new WaitForSeconds(0.5f);
        isDead = true;
        Die();
    }
    public virtual void ApplyDamage(float amount)
    {
        
        SoundManager.PlaySound(SoundManager.Sounds.Hit);
        if (newEnemy)
        {
            Animator.SetTrigger("Hit");
        }
        else
        {
            Animator.SetTrigger("Hurt");
        }
        
        currentHealthEnemy -= amount;
        HPBar.SetHealth(currentHealthEnemy, StaticEnemyStats.enemyMaxHealth);
        if (currentHealthEnemy <= 0)
        {
            StartCoroutine(OneMoneyWait());
            
            
        }
    }

}
