using UnityEngine;
using System.Collections;

public class GhostPowerup : MonoBehaviour
{
    public float ghostDuration = 5f;    // Duration of invincibility
    public float transparency = 0.4f;   // How opaque is the player sprite

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == GameManager.Instance.player)
        {
            SpriteRenderer sr = collision.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                StartCoroutine(GhostEffect(sr));
            }

            Destroy(gameObject);
        }
    }

    private IEnumerator GhostEffect(SpriteRenderer sr)
    {
        Color originalColor = sr.color;

        // Calls isInvincible and iFrameTimer from GameManager
        GameManager.Instance.isInvincible = true;
        GameManager.Instance.iFrameTimer = ghostDuration;

        /* // Player falls through the floor when collier is enables further debugging required
        // Disables the collider 
        Collider2D playerCollider = GameManager.Instance.player.GetComponent<Collider2D>();
        if (playerCollider != null) {
            playerCollider.enabled = false;
        } */

        // Sets translucent 
        Color ghostColor = originalColor;
        ghostColor.a = transparency;
        sr.color = ghostColor;

        // Waits until iFrameTimer runs out
        while (GameManager.Instance.isInvincible)
        {
            yield return null; // wait a frame
        }
        Debug.Log("Ghost effect ended");

        /* // Player falls through the floor when collier is enables further debugging required
        // Re-enables the collider when invincibility ends
        if (playerCollider != null) {
            playerCollider.enabled = true;
        } */

        // Turns player back to fully opaque
        sr.color = originalColor;
    }
}