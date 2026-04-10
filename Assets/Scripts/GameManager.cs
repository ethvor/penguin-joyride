using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // singleton so any script can use GameManager.Instance instead of searching for it
    public static GameManager Instance;

    public GameObject player;             // drag the player object here in the Inspector
    public Vector3 playerStartPos;        // set in Start, used to reset player position

    public float scrollSpeed = 5f;       // how fast the world moves left (units per second)
    public float spawnInterval = 3f;     // seconds between each hazard spawn
    public int playerHP = 1;
    public int hazardDamage = 1;         // hp removed per hit
    public float iFrameDuration = 0.5f;  // seconds of invincibility after getting hit

    public float elapsedTime = 0f;       // how long this run has lasted

    public enum GamePhase { Playing, GameOver, Paused }
    public GamePhase phase = GamePhase.Playing; // current state of the game

    // UnityEvent lets other scripts subscribe with AddListener to get called when something happens
    public UnityEvent onGameOver; // fires once when player dies
    public UnityEvent onReset;    // fires once when level restarts
    public UnityEvent onPause;    // fires when the game is paused
    public UnityEvent onResume;   // fires when the game is unpaused

    public bool isInvincible = false; // true during i-frames, blocks damage
    public float iFrameTimer = 0f;    // counts down i-frame duration

    // initial values saved in Start so reset restores Inspector-tuned values
    private float initScrollSpeed;
    private float initSpawnInterval;
    private int initPlayerHP;

    // saved during pause so resume can restore whatever speed the ramp was at
    private float prePauseScrollSpeed;

    void Awake() // runs before Start — sets up the singleton
    {
        Instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        initScrollSpeed = scrollSpeed;
        initSpawnInterval = spawnInterval;
        initPlayerHP = playerHP;

        if (player != null)
            playerStartPos = player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // only track time while the game is actually running
        if (phase == GamePhase.Playing)
        {
            elapsedTime += Time.deltaTime; //adds time since last frame
        }

        // i-frame countdown — when timer runs out, player can take damage again
        if (isInvincible)
        {
            iFrameTimer -= Time.deltaTime;
            if (iFrameTimer <= 0f)
            {
                isInvincible = false; //return to damageable state after countdown
            }
        }
    }

    // called by PlayerCollision when the player hits a hazard
    public void TakeDamage(int damage)
    {
        if (isInvincible) return;          // still in i-frames, ignore
        if (phase != GamePhase.Playing) return; // game already over, ignore

        playerHP -= damage;

        if (playerHP <= 0)
        {
            OnDeath();
        }
        else
        {
            isInvincible = true;           // start i-frames so overlapping hazards dont instant-kill
            iFrameTimer = iFrameDuration;
        }
    }

    void OnDeath()
    {
        phase = GamePhase.GameOver;
        scrollSpeed = 0f;

        // Invoke() calls every method that subscribed with AddListener
        // other scripts subscribe in their Start like:
        //   GameManager.Instance.onGameOver.AddListener(MyMethod);
        // then MyMethod runs automatically at each game over
        onGameOver.Invoke();
        // dont reset here — wait for the restart button to call ResetLevel
    }

    public void ResetLevel()
    {
        // find and destroy every hazard still in the scene
        GameObject[] hazards = GameObject.FindGameObjectsWithTag("Hazard");
        for (int i = 0; i < hazards.Length; i++)
        {
            Destroy(hazards[i]);
        }

        // reset to Inspector-tuned starting values
        scrollSpeed = initScrollSpeed;
        spawnInterval = initSpawnInterval;
        playerHP = initPlayerHP;
        elapsedTime = 0f;
        isInvincible = false;
        phase = GamePhase.Playing;

        // reset player position and velocity
        if (player != null)
        {
            player.transform.position = playerStartPos;
            player.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
        }

        onReset.Invoke(); // notify all subscribers to reset their own stuff
    }

    public void Pause()
    {
        if (phase != GamePhase.Playing) return; //only pause during play

        prePauseScrollSpeed = scrollSpeed;
        scrollSpeed = 0f;        //freeze scrolling
        phase = GamePhase.Paused;
        onPause.Invoke();
    }

    public void Resume()
    {
        if (phase != GamePhase.Paused) return;

        scrollSpeed = prePauseScrollSpeed; //put the ramped speed back
        phase = GamePhase.Playing;
        onResume.Invoke();
    }

    public void ReturnToMainMenu()
    {
        // Single mode replaces the current scene — destroys everything in scroll-level
        // so no manual cleanup needed, fresh game state on next Start press
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
}
