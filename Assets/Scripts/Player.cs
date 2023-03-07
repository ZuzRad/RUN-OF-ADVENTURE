using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : Character, IDamageable
{
    [Header("Combat")]
    public KeyCode meleeAttack = KeyCode.Mouse0;
    public KeyCode rangeAttack = KeyCode.Mouse1;
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode interaction = KeyCode.E;
    public string xMoveAxis = "Horizontal";
    public Transform meleeAttackOrigin = null;
    public Transform rangeAttackOrigin = null;
    public GameObject projectile = null;
    public float meleeAttackRadius = 0.6f;
    public float meleeAttackDelay = 1.1f;
    public float rangeAttackDelay = 0.3f;
    public LayerMask enemyLayer = 8;
    public HPBar HPBar;

    private LayerMask shopLayer = 11;
    private float moveIntentionX = 0;
    private bool attemptInteraction = false;
    private bool attemptJump = false;
    private bool attemptMeleeAttack = false;
    private bool attemptRangeAttack = false;
    private float timeUntilMeleeReadied = 0;
    private float timeUntilRangeReadied = 0;
    private bool isMeleAttacking = false;
    private float timeStartAttack = 3f;
    
    void Start()
    {
        StaticPlayerStats.currentHealth = StaticPlayerStats.maxHealth;
        HPBar.SetHealth(StaticPlayerStats.currentHealth, StaticPlayerStats.maxHealth);
    }


    void Update()
    {
        timeStartAttack-=Time.deltaTime;
        GetInput();
        HandleJump();
        HandeMeleeAtack();
        HandleRangeAttack();
        HandleAnimations();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Money"))
        {
            SoundManager.PlaySound(SoundManager.Sounds.Money);
            StaticPlayerStats.money++;
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("Gift"))
        {
            SoundManager.PlaySound(SoundManager.Sounds.Money);
            StaticPlayerStats.currentHealth=StaticPlayerStats.maxHealth;
            Destroy(collision.gameObject);
        }
    }

    private void FixedUpdate()
    {
        HandleRun();
    }

    private void GetInput()
    {
        attemptInteraction = Input.GetKeyDown(interaction);
        moveIntentionX = Input.GetAxis(xMoveAxis);
        attemptMeleeAttack = Input.GetKeyDown(meleeAttack);
        attemptJump=Input.GetKeyDown(jumpKey);
        attemptRangeAttack = Input.GetKeyDown(rangeAttack);
    }

    private void HandleRun()
    {    
        if(moveIntentionX>0 && transform.rotation.y == 0 && !isMeleAttacking)
            transform.rotation = Quaternion.Euler(0, 180f, 0);

        else if(moveIntentionX<0 && transform.rotation.y != 0 && !isMeleAttacking)
            transform.rotation = Quaternion.Euler(0, 0, 0);

        Rb.velocity = new Vector2(moveIntentionX * speed, Rb.velocity.y);
    }

    private void HandeMeleeAtack()
    {
        if (attemptMeleeAttack && timeUntilMeleeReadied <= 0 && timeStartAttack <= 0)
        {
            timeUntilMeleeReadied = meleeAttackDelay;
            Collider2D[] overlappedColliders = Physics2D.OverlapCircleAll(meleeAttackOrigin.position, meleeAttackRadius, enemyLayer);
            for(int i = 0; i < overlappedColliders.Length; i++)
            {
                IDamageable enemyAttributes= overlappedColliders[i].GetComponent<IDamageable>();
                if (enemyAttributes != null)
                    enemyAttributes.ApplyDamage(StaticPlayerStats.meleeDamage);
            }
        }
        else
            timeUntilMeleeReadied -= Time.deltaTime;
    }

    private void HandleJump()
    {
        if(attemptJump && CheckGrounded())
        {
            SoundManager.PlaySound(SoundManager.Sounds.Jump);
            Rb.velocity = new Vector2(Rb.velocity.x, jumpForce);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Debug.DrawRay(transform.position, -Vector2.up * groundedLeeway, Color.green);
        if (meleeAttackOrigin != null)
            Gizmos.DrawWireSphere(meleeAttackOrigin.position, meleeAttackRadius);
    }

    private void HandleRangeAttack()
    {
        if (attemptRangeAttack && timeUntilRangeReadied <= 0 && timeStartAttack <= 0)
        {
            Instantiate(projectile, rangeAttackOrigin.position, rangeAttackOrigin.rotation);
            timeUntilRangeReadied = rangeAttackDelay;
        }
        else
            timeUntilRangeReadied -= Time.deltaTime;
    }

    private void HandleAnimations()
    {
        Animator.SetBool("Grounded", CheckGrounded());

        if (attemptMeleeAttack && timeStartAttack <= 0 && !isMeleAttacking)
            StartCoroutine(MeleeAttackDelay());

        if(attemptJump && CheckGrounded() || Rb.velocity.y>1f && !isMeleAttacking)
            Animator.SetTrigger("Jump");
           

        if (attemptRangeAttack && timeStartAttack <= 0)
            Animator.SetTrigger("Shoot");

        if (Mathf.Abs(moveIntentionX) > 0.1f && CheckGrounded())
            Animator.SetInteger("AnimState", 2);
        else
            Animator.SetInteger("AnimState", 0);
    }

    private IEnumerator MeleeAttackDelay()
    {
        Animator.SetTrigger("Attack");
        isMeleAttacking = true;
        yield return new WaitForSeconds(meleeAttackDelay);
        isMeleAttacking = false;
    }

    public virtual void ApplyDamage(float amount)
    {
        SoundManager.PlaySound(SoundManager.Sounds.Hit);
        Animator.SetTrigger("Hurt");
        StaticPlayerStats.currentHealth -= amount;
        HPBar.SetHealth(StaticPlayerStats.currentHealth, StaticPlayerStats.maxHealth);
        if (StaticPlayerStats.currentHealth <= 0) 
            StartCoroutine(DeathWait());
    }
}
