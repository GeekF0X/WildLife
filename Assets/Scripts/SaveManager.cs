using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    static string path = Application.dataPath + "/game.sav";

    public static void Save(SaveData save)
    {
        Global.saveScene = save.sceneData;
        Global.achievment = save.achievDava;

        Debug.Log("salvando em: " + path);
        string data = JsonUtility.ToJson(save);
        File.WriteAllText(path, data);
    }

    public static SaveData Load()
    {
        if (File.Exists(path))
        {
            string data = File.ReadAllText(path);
            SaveData save = JsonUtility.FromJson<SaveData>(data);

            return save;
        }
        else
        {
            var newData = new SaveData();
            newData.achievDava = new AchievData();
            newData.achievDava.achievName = new();
            newData.achievDava.achievValue = new();
            Save(newData);
            return newData;
        }
    }
}
