using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehavior : MonoBehaviour
{
    public int hp;
    public Animator anim;
    public float speed;
    private int multiplier;
    
    private Rigidbody2D rb;
    
    public ParticleSystem jp;
    public ParticleSystem die;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hp = 1;
        rb = GetComponent<Rigidbody2D>();
        multiplier = 10;
        //anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hp <= 0)
        {
            die.Play();
        }
        if(Keyboard.current.upArrowKey.wasPressedThisFrame)
        {
            jp.Play();
        }
        
        if(Keyboard.current.upArrowKey.isPressed)
        {
            rb.gravityScale = 0.0f;
            rb.linearVelocity = new Vector2(0.0f, 0.0f);
            //rb.mass = 0.0f;
            //float newY = transform.position.y + speed;
            //transform.position = new Vector2(transform.position.x, newY);
            rb.AddForceY(speed * multiplier * Time.deltaTime,ForceMode2D.Impulse);
        }
        
        if(Keyboard.current.upArrowKey.wasReleasedThisFrame)
        {
            rb.gravityScale = 0.5f;
            rb.linearVelocity = new Vector2(0.0f, 0.0f);
            jp.Stop();
        }
        
        if(Keyboard.current.downArrowKey.isPressed)
        {
            //rb.gravityScale += 0.5f;
            rb.gravityScale = 0.0f;
            rb.AddForceY(-0.5f*speed * multiplier * Time.deltaTime,ForceMode2D.Impulse);
        }
    }
    
    public void onCollisionEnter2D(Collision other)
    {
        if (other.gameObject.tag.Equals("floor"))
        {
            rb.gravityScale = 0.5f;
        }
    }
}
