using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehavior : MonoBehaviour
{
    public int hp;
    public Animator anim;
    public float speed;
    
    private Rigidbody2D rb;
    private bool top;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        top = false;
        //anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Keyboard.current.upArrowKey.isPressed  && !top)
        {
            rb.gravityScale = 0.0f;
            rb.mass = 0.0f;
            rb.linearVelocity = new Vector2(0.0f, 0.0f);
            float newY = transform.position.y + speed;
            transform.position = new Vector2(transform.position.x, newY);
        }
        if(Keyboard.current.upArrowKey.wasReleasedThisFrame)
        {
            rb.gravityScale = 1.0f;
            rb.mass = 1.0f;
        }
        if(Keyboard.current.downArrowKey.isPressed)
        {
            rb.gravityScale += 0.5f;
        }
    }
    
    public void onCollisionEnter2D(Collision other)
    {
        if (other.gameObject.tag.Equals("floor"))
        {
            rb.gravityScale = 1.0f;
        }

        if (other.gameObject.tag.Equals("ceiling"))
        {
            top = true;
            rb.gravityScale = 1.0f;
            rb.mass = 1.0f;
        }
    }
    
    public void onCollisionExit2D(Collision other)
    {
        if(other.gameObject.tag.Equals("ceiling"))
        {
            top = false;
        }
    }
}
