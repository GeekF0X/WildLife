using UnityEngine;
using UnityEngine.SceneManagement;


public class Global
{
    static public SceneData saveScene;
    static public AchievData achievment;
    public static bool FromStar { get; private set; } = false;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void InitializeOnStartup()
    {
        var data = SaveManager.Load();
        saveScene = data.sceneData;
        achievment = data.achievDava;
        Debug.Log(JsonUtility.ToJson(saveScene, true));
        Debug.Log(saveScene.checkpoint);
        FromStar = false;
    }

    public static void LoadScene(string levelName)
    {
        FromStar = true;

        SaveData data = new();
        data.sceneData = new();
        data.achievDava = achievment;
        SaveManager.Save(data);

        SceneManager.LoadScene(levelName);
    }

    public static void LoadCheckpoint()
    {
        FromStar = false;
        SceneManager.LoadScene(saveScene.level);
    }
}
