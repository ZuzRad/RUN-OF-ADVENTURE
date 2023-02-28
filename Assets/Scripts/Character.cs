using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
 
    [Header("Movement")]
    public float speed = 5f;
    public float jumpForce = 6f;
    public float groundedLeeway = 0.1f;
    private Rigidbody2D rb = null;
    
    private Animator animator = null;
    private SoundManager soundManager;

    public SoundManager SoundManager
    {
        get { return soundManager; }
        protected set { soundManager = value; }
    }
   
    public Animator Animator
    {
        get { return animator; }
        protected set { animator = value; }
    }
    public Rigidbody2D Rb
    {
        get { return rb; }
        protected set { rb = value; }
    }



    void Awake()
    {
        soundManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
        if (GetComponent<Rigidbody2D>())
        {
            rb=GetComponent<Rigidbody2D>();
        }

        if (GetComponent<Animator>())
        {
            animator = GetComponent<Animator>();
        }
    }

    protected virtual void Die()
    {
        gameObject.SetActive(false);
        Destroy(gameObject);
    }

    protected IEnumerator DeathWait()
    {
        Animator.SetTrigger("Death");
        yield return new WaitForSeconds(0.5f);
        Die();
    }

    protected bool CheckGrounded()
    {
        return Physics2D.Raycast(transform.position, -Vector2.up, groundedLeeway);
    }
}