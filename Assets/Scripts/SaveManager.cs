using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    static string path = Application.dataPath + "/game.sav";

    public static void Save(SaveData save)
    {
        Debug.Log("salvando em: " + path);
        string data = JsonUtility.ToJson(save);
        File.WriteAllText(path, data);
    }

    public static SaveData Load()
    {
        string data = File.ReadAllText(path);
        SaveData save = JsonUtility.FromJson<SaveData>(data);

        return save;
    }
}
