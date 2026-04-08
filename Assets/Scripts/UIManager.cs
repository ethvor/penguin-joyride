using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject deathScreen;
    public GameObject startScreen;

    void Start()
    {
        deathScreen.SetActive(false); // hides death screen at launch
        startScreen.SetActive(true); // show start screen at launch

        // subscribe to GameManager events
        GameManager.Instance.onGameOver.AddListener(ShowDeathScreen);
        GameManager.Instance.onReset.AddListener(HideDeathScreen);
    }

    void ShowDeathScreen()
    {
        deathScreen.SetActive(true);
    }

    void HideDeathScreen()
    {
        deathScreen.SetActive(false);
    }

    public void OnStartPressed()
    {
        startScreen.SetActive(false);
        GameManager.Instance.phase = GameManager.GamePhase.Playing;
    }

    public void OnRestartPressed()
    {
        GameManager.Instance.ResetLevel();
    }
}