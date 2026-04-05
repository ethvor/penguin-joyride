using UnityEngine;
using UnityEngine.InputSystem;

public class DummyPlayerMovement : MonoBehaviour
{
    public float thrustForce = 0.6f;
    private Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.spaceKey.isPressed)
        {
            rb.AddForce(Vector2.up * thrustForce);
        }
    }
}
