using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HoverReveal : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image revealImage;

    void Start()
    {
        if (revealImage != null)
            revealImage.enabled = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (revealImage != null)
            revealImage.enabled = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (revealImage != null)
            revealImage.enabled = false;
    }
}
