using UnityEngine;

public class HazardSpawner : MonoBehaviour
{
    public GameObject[] easyPatterns;   //prefabs for early game hazards
    public GameObject[] mediumPatterns; //prefabs that unlock after mediumThreshold seconds
    public GameObject[] hardPatterns;   //prefabs that unlock after hardThreshold seconds

    public GameObject[] testPatterns;    //drag patterns here to only spawn these, leave empty for normal pools

    public float spawnX = 15f;         //x position where patterns spawn (offscreen to right)
    private float spawnTimer = 0f;     //time between each spawn, resets on hazard spawned, not a param

    // how many seconds of elapsed time before medium and hard patterns start appearing
    public float mediumThreshold = 30f;
    public float hardThreshold = 60f;

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.phase != GameManager.GamePhase.Playing) return; //stop hazard from spawning after death and when not playing

        spawnTimer -= Time.deltaTime;

        if (spawnTimer <= 0f)
        {
            SpawnPattern();
            spawnTimer = GameManager.Instance.spawnInterval; //manager may ramp / dampen spawn speed later in level
        }
    }

    void SpawnPattern()
    {
        GameObject pattern = PickPattern();
        if (pattern == null) return;

        // keep the prefab's own Y so patterns can be positioned vertically in the editor
        float prefabY = pattern.transform.position.y;
        Vector3 spawnPos = new Vector3(spawnX, prefabY, 0f);
        Instantiate(pattern, spawnPos, Quaternion.identity);
    }

    GameObject PickPattern()
    {
        if (testPatterns != null && testPatterns.Length > 0)
            return testPatterns[Random.Range(0, testPatterns.Length)];
        float elapsed = GameManager.Instance.elapsedTime; //get game run time from manager

        // build a list of which pools are available based on elapsed time
        // always have easy, add medium and hard as time passes
        int poolCount = 1;
        if (elapsed >= mediumThreshold && mediumPatterns.Length > 0) poolCount = 2; //med available after threshold reached
        if (elapsed >= hardThreshold && hardPatterns.Length > 0) poolCount = 3; //hard available after threshold reached

        // pick a random pool, weighted toward harder ones as more unlock
        // with 3 pools: 33% easy, 33% medium, 33% hard
        int poolChoice = Random.Range(0, poolCount); //select a category

        if (poolChoice == 0 && easyPatterns.Length > 0)
        {
            return easyPatterns[Random.Range(0, easyPatterns.Length)]; //rand within easy patterns
        }
        else if (poolChoice == 1 && mediumPatterns.Length > 0)
        {
            return mediumPatterns[Random.Range(0, mediumPatterns.Length)]; //rand within med patterns
        }
        else if (poolChoice == 2 && hardPatterns.Length > 0)
        {
            return hardPatterns[Random.Range(0, hardPatterns.Length)]; //rand within hard patterns
        }

        //easy if havent made non easy patterns
        if (easyPatterns.Length > 0)
        {
            return easyPatterns[Random.Range(0, easyPatterns.Length)];
        }

        return null; // unity yells if dont have a return for every case (this is no patterns exist case)
    }
}
