using UnityEngine;

// example script showing how to subscribe to game manager events
public class ExampleSubscriber : MonoBehaviour
{
    public GameObject someCanvas;

    void Start()
    {
        someCanvas.SetActive(false);

        // subscribe by adding a listener.
        // when onGameOver fires, ShowSomeScreen runs
        GameManager.Instance.onGameOver.AddListener(ShowSomeScreen); //fire ShowSomeScreen when onGameOver happens in game manager
        GameManager.Instance.onReset.AddListener(HideSomeScreen);
    }

    void ShowSomeScreen()
    {
        someCanvas.SetActive(true);
    }

    void HideSomeScreen()
    {
        someCanvas.SetActive(false);
    }
}
