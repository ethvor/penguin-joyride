using UnityEngine;

public class PlayerCollision : MonoBehaviour
{     
    void OnTriggerEnter2D(Collider2D other) // on collision with 'trigger' enabled collider
    {
        if (other.transform.root.CompareTag("Hazard"))
        {
            GameManager.Instance.TakeDamage(GameManager.Instance.hazardDamage); //deal damage using game manager
        }

        //collision with player and coin
        if(other.gameObject.tag == "Coin") {
            CoinManager coinmanager = other.GetComponent<CoinManager>();
            CoinBehaviour coin = other.GetComponent<CoinBehaviour>(); 

            if(coinmanager != null) {
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
            CoinManager coinmanager = other.GetComponent<CoinManager>();
            CoinMultiplier multiCoin = other.GetComponent<CoinMultiplier>(); 

            if(coinmanager != null) {
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

