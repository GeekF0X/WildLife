using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class EventSystemManager : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
    }

    private void Start()
    {
        EnsureSingleEventSystem();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        EnsureSingleEventSystem();
    }

    private void OnSceneUnloaded(Scene scene)
    {
        EnsureSingleEventSystem();
    }

    private void EnsureSingleEventSystem()
    {
        EventSystem[] allEventSystems = FindObjectsByType<EventSystem>(FindObjectsSortMode.None);

        if (allEventSystems.Length == 0)
        {
            Debug.LogWarning("[EventSystemManager] Nenhum EventSystem encontrado!");
            return;
        }

        bool keptOne = false;

        foreach (EventSystem es in allEventSystems)
        {
            if (es == null) continue;

            if (!keptOne)
            {
                es.enabled = true;
                keptOne = true;
                Debug.Log($"[EventSystemManager] EventSystem ativo: {es.gameObject.name}");
            }
            else
            {
                es.enabled = false;
                Debug.Log($"[EventSystemManager] EventSystem desativado: {es.gameObject.name}");
            }
        }
    }
}