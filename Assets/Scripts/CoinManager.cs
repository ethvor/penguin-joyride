using UnityEngine;
using TMPro; 

public class CoinManager : MonoBehaviour
{
    public int totalCoins; 
    private int currMultiplier;
    private float multiplierTimer; 
    public TMP_Text scoreText;  
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        totalCoins = 0; 
        currMultiplier = 1; 
        multiplierTimer = 0f; 
        updateScore(); 
    }

    // Update is called once per frame
    void Update()
    {
        if(multiplierTimer > 0f) {
            multiplierTimer -= Time.deltaTime; 
            if(multiplierTimer <= 0f) {
                currMultiplier = 1; 
            }
        }
        
    }
    public void AddCoins(int amount) {
        totalCoins += amount * currMultiplier;
        updateScore(); 
    }
    public void ActivateMultiplier(int multiplier, float duration) {
        currMultiplier = multiplier; 
        multiplierTimer = duration; 
    }
    public void updateScore() {
        if(scoreText != null) {
            scoreText.SetText("Coins: " + totalCoins); 
        }
    }

}
