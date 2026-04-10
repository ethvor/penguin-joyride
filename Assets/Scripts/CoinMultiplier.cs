using UnityEngine;

public class CoinMultiplier : MonoBehaviour
{
    public float duration; 
    public int multiplier; 
    public AudioClip multiplierAudio; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        duration = 15f; 
        multiplier = 2; 
    }

}
