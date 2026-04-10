using UnityEngine;
using TMPro;

public class PlayerCollision : MonoBehaviour
{
    private CoinManager coinmanager; 
    void Start() {
        coinmanager = GetComponent<CoinManager>(); 
    }
    
    void OnTriggerEnter2D(Collider2D other) // on collision with 'trigger' enabled collider
    {
        // anything inside a hazard pattern kills UNLESS its a coin or powerup
        if (other.transform.root.CompareTag("Hazard")
            && !other.CompareTag("Coin")
            && !other.CompareTag("CoinMulti")
            && !other.CompareTag("Powerup"))
        {
            GameManager.Instance.TakeDamage(GameManager.Instance.hazardDamage);
        }

        //collision with player and coin
        if(other.gameObject.tag == "Coin") {
            CoinBehaviour coin = other.GetComponent<CoinBehaviour>(); 

            if(coinmanager != null && coin != null) {
                coinmanager.AddCoins(coin.coinValue); 
            }
            //Play audio
            float volume = 1; 
            AudioSource.PlayClipAtPoint(coin.coinAudio, other.transform.position, volume); 
            
            //Destroy coin 
            Destroy(other.gameObject); 
        }
        //collision with player and coin multiplier
        if(other.gameObject.tag == "CoinMulti") {
            CoinMultiplier multiCoin = other.GetComponent<CoinMultiplier>(); 

            if(coinmanager != null && multiCoin != null) {
                coinmanager.ActivateMultiplier(multiCoin.multiplier, multiCoin.duration); 
            }
            //Play audio
            float volume = 1; 
            AudioSource.PlayClipAtPoint(multiCoin.multiplierAudio, other.transform.position, volume); 
            
            //Destroy multiplier coin 
            Destroy(other.gameObject); 
        }

    }
}

