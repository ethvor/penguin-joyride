using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuLoader : MonoBehaviour
{
    // hooked to the Start button's OnClick in the Inspector
    public void OnStartPressed()
    {
        // SceneManager.LoadScene with Single replaces the menu scene with the game scene
        // standard way to transition between scenes from a button click
        SceneManager.LoadScene("scroll-level", LoadSceneMode.Single);
    }
}
