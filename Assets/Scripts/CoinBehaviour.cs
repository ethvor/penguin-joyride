using UnityEngine;
using UnityEngine.InputSystem;

public class CoinBehaviour : MonoBehaviour
{
    private int coinValue = 1;
    public AudioSource coinAudio; 
    public AudioClip coinClip;
    void Start() {
        coinAudio = GetComponent<AudioSource>(); 
    }
    
    //Collision of player and coin
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")) {
            CoinManager coinmanager = other.GetComponent<CoinManager>(); 
            if(coinmanager != null) {
                coinmanager.AddCoins(coinValue); 
            }
            //Play audio
            coinAudio.PlayOneShot(coinClip);
            
            //Destroy coin 
            Destroy(gameObject); 
        }

    }
}
