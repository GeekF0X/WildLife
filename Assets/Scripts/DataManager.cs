using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataManager : MonoBehaviour
{
    public List<Checkpoint> levelCheckpoints;
    public int checkpointIndex;
#nullable enable
    public Robot? small, big;

    private void Awake()
    {
        if (Global.saveScene == null || Global.FromStar)
        {
            SaveData data = new();
            data.sceneData = new();
            data.achievDava = new();
            data.sceneData.level = SceneManager.GetActiveScene().name;
            data.sceneData.small = new RobotDataAdapter(small);
            data.sceneData.big = new RobotDataAdapter(big);
            data.achievDava.achievName = new List<string>();
            data.achievDava.achievValue = new List<bool>();
            SaveManager.Save(data);
        }
        else
        {
            checkpointIndex = Global.saveScene.checkpoint;
            for (int i = 0; i < checkpointIndex; i++)
            {
                levelCheckpoints[i].gameObject.SetActive(false);
                levelCheckpoints[i].puzzle.SetActive(false);
            }
            if (small)
            {
                RobotDataAdapter robot = new(Global.saveScene.small);
                robot.LoadRobot(ref small);
            }
            if (big)
            {
                RobotDataAdapter robot = new(Global.saveScene.big);
                robot.LoadRobot(ref big);
            }
        }
    }

    public void SaveCheckpoint(Checkpoint checkpoint)
    {
        SaveData data = new();
        data.sceneData = Global.saveScene;
        data.achievDava = Global.achievment;

        data.sceneData.level = SceneManager.GetActiveScene().name;
        data.sceneData.checkpoint = checkpointIndex;
        data.sceneData.small = new RobotDataAdapter(checkpoint.smallPosition);
        data.sceneData.big = new RobotDataAdapter(checkpoint.bigPosition);

        SaveManager.Save(data);
    }

    public void AddachiveList(GameObject obj)
    {
        SaveData data = SaveManager.Load();

        data.achievDava.achievName.Add(obj.name);
        data.achievDava.achievValue.Add(false);
        SaveManager.Save(data);
    }

    public void SaveAchievment(int i)
    {
        SaveData data = new();
        data.sceneData = Global.saveScene;
        data.achievDava = Global.achievment;

        data.achievDava.achievValue[i] = true;
        SaveManager.Save(data);
    }

}
