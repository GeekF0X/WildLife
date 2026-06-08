using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataManager : MonoBehaviour
{
    string level;
    public List<Checkpoint> levelCheckpoints;
    public int checkpointIndex;
#nullable enable
    public Robot? small, big;

    private void OnEnable()
    {
        if (SaveManager.Load() == null)
        {
            Debug.Log("coe");
            
        }
        else
        {
            LoadGame();
            level = SceneManager.GetActiveScene().name;
            checkpointIndex = Global.saveScene.checkpoint;
            for (int i = 0; i < checkpointIndex; i++)
            {
                Debug.Log(checkpointIndex);
                levelCheckpoints[i].gameObject.SetActive(false);
                levelCheckpoints[i].puzzle.SetActive(false);
            }
            if (small)
            {
                RobotDataAdapter robot = new(Global.saveScene.small);
                Debug.Log(robot.position);
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
        data.sceneData = new();
        data.achievDava = Global.achievment;

        data.sceneData.level = SceneManager.GetActiveScene().name;
        data.sceneData.checkpoint = checkpointIndex;
        data.sceneData.small = new RobotDataAdapter(checkpoint.smallPosition);
        data.sceneData.big = new RobotDataAdapter(checkpoint.bigPosition);

        SaveManager.Save(data);
    }

    public void LoadGame()
    {
        SaveData data = SaveManager.Load();
        Debug.Log(data.sceneData);
        Debug.Log(data.sceneData.level);
        Global.saveScene = data.sceneData;

        //SceneManager.LoadScene(data.sceneData.level);
        
    }

}
