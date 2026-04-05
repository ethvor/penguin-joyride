using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    
    
    
    
    void OnTriggerEnter2D(Collider2D other) // on collision with 'trigger' enabled collider
    {
        if (other.transform.root.CompareTag("Hazard"))
        {
            GameManager.Instance.TakeDamage(GameManager.Instance.hazardDamage); //deal damage using game manager
        }
    }
}
