using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehavior : MonoBehaviour
{
    public Animator anim;
    public float speed;
    //private int multiplier;
    public float down;
    private bool dead;
    
    private bool first;
    public AudioSource boom;
    public AudioSource rev;
    public AudioSource bubble;
    public float revDuration = 0.58f;
    
    private Rigidbody2D rb;
    
    public ParticleSystem jp;
    public ParticleSystem die;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        dead = false;
        first = true;
        //multiplier = 10;
        anim = GetComponent<Animator>();
        // subscribe to GameManager events
        GameManager.Instance.onGameOver.AddListener(dieVFX);
        GameManager.Instance.onReset.AddListener(restart);

        // play startup sound immediately on level load, cut before bubble tail
        rev.Play();
    }

    // Update is called once per frame
    void Update()
    {
        // cut rev at exact playback position instead of wall clock
        if (rev.isPlaying && rev.time >= revDuration)
        {
            rev.Stop();
        }

        if (!dead)
        {
            if (Keyboard.current.upArrowKey.wasPressedThisFrame || Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                // only play bubble after startup rev finishes
                if (!rev.isPlaying) {
                    bubble.Play();
                }
                anim.SetTrigger("fly");
                jp.Play();
            }

            if (Keyboard.current.downArrowKey.wasPressedThisFrame || Keyboard.current.shiftKey.wasPressedThisFrame)
            {
                rev.Stop();
                bubble.Stop();
                anim.SetTrigger("fall");
            }

            if (Keyboard.current.upArrowKey.wasReleasedThisFrame || Keyboard.current.spaceKey.wasReleasedThisFrame)
            {
                rev.Stop();
                bubble.Stop();
                anim.SetTrigger("fall");
                rb.gravityScale = 0.5f;
                rb.linearVelocity = new Vector2(0.0f, 0.0f);
                jp.Stop();
            }

            if (Keyboard.current.downArrowKey.wasReleasedThisFrame || Keyboard.current.shiftKey.wasReleasedThisFrame)
            {
                rb.gravityScale = 0.5f;
                rb.linearVelocity = new Vector2(0.0f, 0.0f);
            }
        }
    }

    public void FixedUpdate()
    {
        if (!dead)
        {
            if (Keyboard.current.upArrowKey.isPressed || Keyboard.current.spaceKey.isPressed)
            {
                if(!rev.isPlaying && !bubble.isPlaying){
                    bubble.Play();
                }
                rb.gravityScale = 0.0f;
                rb.linearVelocity = new Vector2(0.0f, 0.0f);
                rb.AddForceY(speed * Time.fixedDeltaTime, ForceMode2D.Impulse);
            }

            if (Keyboard.current.downArrowKey.isPressed || Keyboard.current.shiftKey.isPressed)
            {
                rb.gravityScale = 0.0f;
                rb.AddForceY(-down * Time.fixedDeltaTime, ForceMode2D.Impulse);
            }
        }
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("floor") && !dead)
        {
            anim.SetTrigger("run");
        }
    }

    void dieVFX()
    {
        dead = true;
        die.Play();
        boom.Play();
        rev.Stop();
        bubble.Stop();
        jp.Stop();
        GetComponent<SpriteRenderer>().enabled = false;
        anim.SetTrigger("fall");

    }

    void restart()
    {
        dead = false;
        rev.Stop();
        bubble.Stop();
        jp.Stop();
        GetComponent<SpriteRenderer>().enabled = true;
        rb.gravityScale = 0.5f;
        anim.SetTrigger("run");

        // replay startup rev like a fresh level load
        rev.Play();
    }
}
