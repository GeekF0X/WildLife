using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class CreditsNavigator : MonoBehaviour
{
    [SerializeField] private string targetSceneName;

    public void GoToScene()
    {
        if (string.IsNullOrEmpty(targetSceneName))
        {
            Debug.LogError("[CreditsNavigator] targetSceneName não foi definido!");
            return;
        }

        StartCoroutine(SwitchSceneRoutine());
    }

    private IEnumerator SwitchSceneRoutine()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        Debug.Log($"[CreditsNavigator] Trocando de '{currentScene}' para '{targetSceneName}'");

        SceneManager.LoadScene(targetSceneName, LoadSceneMode.Additive);

        yield return null;

        Scene newScene = SceneManager.GetSceneByName(targetSceneName);
        if (newScene.IsValid())
            SceneManager.SetActiveScene(newScene);

        DisableDuplicates();

        AsyncOperation unload = SceneManager.UnloadSceneAsync(currentScene);
        while (unload != null && !unload.isDone)
            yield return null;

        Debug.Log($"[CreditsNavigator] Agora em '{targetSceneName}'");
    }

    private void DisableDuplicates()
    {
        var eventSystems = FindObjectsByType<UnityEngine.EventSystems.EventSystem>(FindObjectsSortMode.None);
        if (eventSystems.Length > 1)
            for (int i = 1; i < eventSystems.Length; i++)
                if (eventSystems[i] != null) eventSystems[i].enabled = false;

        var listeners = FindObjectsByType<AudioListener>(FindObjectsSortMode.None);
        if (listeners.Length > 1)
            for (int i = 1; i < listeners.Length; i++)
                if (listeners[i] != null) listeners[i].enabled = false;
    }
}