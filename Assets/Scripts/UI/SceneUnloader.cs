using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneUnloader : MonoBehaviour
{
    public void UnloadCurrentScene()
    {
        StartCoroutine(UnloadRoutine());
    }

    private IEnumerator UnloadRoutine()
    {
        string currentScene = SceneManager.GetActiveScene().name;

        Scene previousScene = SceneManager.GetSceneAt(0);
        if (previousScene.IsValid())
        {
            SceneManager.SetActiveScene(previousScene);
            Debug.Log($"[SceneUnloader] Active scene: '{previousScene.name}'");
        }

        if (SceneLoader.Instance != null)
            SceneLoader.Instance.ReactivateObjects();

        yield return null;

        if (previousScene.name == currentScene)
            currentScene = SceneManager.GetSceneAt(1).name;
        AsyncOperation unload = SceneManager.UnloadSceneAsync(currentScene);
        while (unload != null && !unload.isDone)
            yield return null;


        Debug.Log($"[SceneUnloader] Scene '{currentScene}' descarregada.");
    }
}