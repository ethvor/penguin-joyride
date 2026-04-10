using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public int totalCoins = 0; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AddCoins(int amount) {
        totalCoins += amount; 
    }

}
