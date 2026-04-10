using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    public GameObject deathScreen;  // toggled by onGameOver / onReset from GameManager
    public GameObject pauseScreen;  // toggled by onPause / onResume from GameManager

    void Start()
    {
        deathScreen.SetActive(false); // hides death screen at launch
        pauseScreen.SetActive(false); // pause panel starts hidden

        // AddListener attaches a method to a UnityEvent's call list
        // when GameManager calls Invoke() on the event, attached method runs once
        GameManager.Instance.onGameOver.AddListener(ShowDeathScreen);
        GameManager.Instance.onReset.AddListener(HideDeathScreen);
        GameManager.Instance.onPause.AddListener(ShowPauseScreen);
        GameManager.Instance.onResume.AddListener(HidePauseScreen);
    }

    void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            // same key handles both directions — pause from playing, resume from paused
            // gameover phase ignores escape
            if (GameManager.Instance.phase == GameManager.GamePhase.Playing)
            {
                GameManager.Instance.Pause();
            }
            else if (GameManager.Instance.phase == GameManager.GamePhase.Paused)
            {
                GameManager.Instance.Resume();
            }
        }
    }

    void ShowDeathScreen()
    {
        deathScreen.SetActive(true);
    }

    void HideDeathScreen()
    {
        deathScreen.SetActive(false);
    }

    void ShowPauseScreen()
    {
        pauseScreen.SetActive(true);
    }

    void HidePauseScreen()
    {
        pauseScreen.SetActive(false);
    }

    // the OnXPressed methods below are public so Unity Buttons can hook them up
    // via the Inspector's OnClick list — Unity finds them by reflection on the component
    public void OnRestartPressed()
    {
        GameManager.Instance.ResetLevel();
    }

    public void OnResumePressed()
    {
        GameManager.Instance.Resume();
    }

    public void OnResetPressed()
    {
        pauseScreen.SetActive(false); // hide pause before reset so its not stuck over the level
        GameManager.Instance.ResetLevel();
    }

    public void OnMainMenuPressed()
    {
        GameManager.Instance.ReturnToMainMenu(); // loads MainMenu scene, this whole UI goes with it
    }
}
