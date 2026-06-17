using UnityEngine;
using UnityEngine.EventSystems;


public class UIButtonSFX : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    [Tooltip("Se true, toca som de hover ao passar o mouse")]
    public bool playHoverSound = true;

    [Tooltip("Se true, toca som de click")]
    public bool playClickSound = true;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (playHoverSound && AudioManager.Instance != null)
            AudioManager.Instance.PlayUIHover();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (playClickSound && AudioManager.Instance != null)
            AudioManager.Instance.PlayUIClick();
    }
}