using UnityEngine;

public class PowerupSpawner : MonoBehaviour
{
    public GameObject powerupPrefab;

    public float minSpawnTime = 1f; // Minimum spawn time for powerups
    public float maxSpawnTime = 3f; // Maximum spawn time for powerups

    public float minY = -3.5f; // Minimum hieght for spawner
    public float maxY = 4f; // Maximum hieght for spawner

    private float nextSpawn;

    void Start()
    {
        SetNextSpawnTime();
    }

    void Update()
    {
        if (Time.time >= nextSpawn)
        {
            SpawnPowerup();
            SetNextSpawnTime();
        }
    }

    void SpawnPowerup() 
    {
        float rand = Random.Range(minY, maxY);

    }

    void SetNextSpawnTime() 
    {

    }
}