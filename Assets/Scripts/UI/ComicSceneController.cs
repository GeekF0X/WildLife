using UnityEngine;

public class ComicSceneController : MonoBehaviour
{
    private const string UNLOCK_REASON = "comic";

    private void OnEnable()
    {
        if (MouseController.Instance != null)
            MouseController.Instance.RequestUnlock(UNLOCK_REASON);
    }

    private void OnDisable()
    {
        if (MouseController.Instance != null)
            MouseController.Instance.ReleaseUnlock(UNLOCK_REASON);
    }

    public void OnContinueButton()
    {
        gameObject.SetActive(false);

        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayUIClick();
    }
}