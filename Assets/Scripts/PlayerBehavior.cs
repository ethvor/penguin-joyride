using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehavior : MonoBehaviour
{
    public Animator anim;
    public float speed;
    //private int multiplier;
    public float down;
    
    private Rigidbody2D rb;
    
    public ParticleSystem jp;
    public ParticleSystem die;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //multiplier = 10;
        anim = GetComponent<Animator>();
        // subscribe to GameManager events
        GameManager.Instance.onGameOver.AddListener(dieVFX);
    }

    // Update is called once per frame
    void Update()
    {
        if(Keyboard.current.upArrowKey.wasPressedThisFrame|| Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            anim.SetTrigger("fly");
            jp.Play();
        }
        
        if(Keyboard.current.downArrowKey.wasPressedThisFrame|| Keyboard.current.shiftKey.wasPressedThisFrame)
        {
            anim.SetTrigger("fall");
        }
        
        if(Keyboard.current.upArrowKey.wasReleasedThisFrame || Keyboard.current.spaceKey.wasReleasedThisFrame)
        {
            anim.SetTrigger("fall");
            rb.gravityScale = 0.5f;
            rb.linearVelocity = new Vector2(0.0f, 0.0f);
            jp.Stop();
        }

        if(Keyboard.current.downArrowKey.wasReleasedThisFrame || Keyboard.current.shiftKey.wasReleasedThisFrame)
        {
            rb.gravityScale = 0.5f;
            rb.linearVelocity = new Vector2(0.0f, 0.0f);
        }
    }

    public void FixedUpdate()
    {
        if(Keyboard.current.upArrowKey.isPressed || Keyboard.current.spaceKey.isPressed)
        {
            rb.gravityScale = 0.0f;
            rb.linearVelocity = new Vector2(0.0f, 0.0f);
            rb.AddForceY(speed * Time.fixedDeltaTime,ForceMode2D.Impulse);
        }
        
        if(Keyboard.current.downArrowKey.isPressed || Keyboard.current.shiftKey.isPressed)
        {
            rb.gravityScale = 0.0f;
            rb.AddForceY(-down * Time.fixedDeltaTime,ForceMode2D.Impulse);
        }
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("floor"))
        {
            anim.SetTrigger("run");
        }
    }

    void dieVFX()
    {
        die.Play();
    }
}
