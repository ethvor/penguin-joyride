using UnityEngine;

public class CoinMulti : MonoBehaviour
{
    public float duration; 
    public int multiplier; 
    public AudioSource powerupSource; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        duration = 15f; 
        multiplier = 2; 
        powerupSource = GetComponent<AudioSource>(); 
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")) {
            CoinManager coinmanager = other.GetComponent<CoinManager>(); 
            if(coinmanager != null) {
                coinmanager.ActivateMultiplier(multiplier, duration); 
            }
            //Play audio
            powerupSource.Play();
            
            //Destroy multiplier coin 
            Destroy(gameObject); 
        }

    }

}
