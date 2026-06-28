using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance { get; private set; }

    [SerializeField] private string mainSceneName = "Blocagem2";

    [SerializeField]
    private List<GameObject> objectsToDeactivate = new List<GameObject>();

    private float previousTimeScale = 1f;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void LoadSceneAdditive(string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName));
    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        previousTimeScale = Time.timeScale;
        Time.timeScale = 1f;

        DeactivateObjects();

        yield return null;

        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);

        yield return null;

        Scene loadedScene = SceneManager.GetSceneByName(sceneName);
        if (loadedScene.IsValid())
            SceneManager.SetActiveScene(loadedScene);

        DisableDuplicateEventSystem();
        DisableDuplicateAudioListener();

        Debug.Log($"[SceneLoader] Scene '{sceneName}' carregada.");
    }

    private void DeactivateObjects()
    {
        foreach (GameObject obj in objectsToDeactivate)
        {
            if (obj != null)
                obj.SetActive(false);
        }
        Debug.Log($"[SceneLoader] {objectsToDeactivate.Count} objetos desativados.");
    }

    public void ReactivateObjects()
    {
        foreach (GameObject obj in objectsToDeactivate)
        {
            if (obj != null)
                obj.SetActive(true);
        }
        Debug.Log($"[SceneLoader] {objectsToDeactivate.Count} objetos reativados.");
    }

    private void DisableDuplicateEventSystem()
    {
        EventSystem[] eventSystems = FindObjectsByType<EventSystem>(FindObjectsSortMode.None);
        if (eventSystems.Length > 1)
        {
            for (int i = 1; i < eventSystems.Length; i++)
                if (eventSystems[i] != null) eventSystems[i].enabled = false;
        }
    }

    private void DisableDuplicateAudioListener()
    {
        AudioListener[] listeners = FindObjectsByType<AudioListener>(FindObjectsSortMode.None);
        if (listeners.Length > 1)
        {
            for (int i = 1; i < listeners.Length; i++)
                if (listeners[i] != null) listeners[i].enabled = false;
        }
    }
}