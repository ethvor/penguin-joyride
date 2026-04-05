using UnityEngine;

public class Scroll : MonoBehaviour
{
    public float destroyX = -20f;

    // Update is called once per frame
    void Update()
    {
        // Vector3.left is (-1, 0, 0) — direction only, scrollSpeed controls how many units per second
        transform.position += Vector3.left * GameManager.Instance.scrollSpeed * Time.deltaTime;  

        if (transform.position.x < destroyX)
        {
            Destroy(gameObject);
        }
    }
}
