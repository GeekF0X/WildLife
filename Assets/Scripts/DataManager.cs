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
        level = SceneManager.GetActiveScene().name;
        checkpointIndex = Global.saveScene.checkpoint;
        for(int i = 0; i < checkpointIndex; i++)
        {
            levelCheckpoints[i].gameObject.SetActive(false);
            levelCheckpoints[i].puzzle.SetActive(false);
        }
        if (small)
        {
            small.controller.enabled = false;
            small.transform.position = Global.saveScene.small.position;
            small.transform.rotation = Global.saveScene.small.rotation;
            small.controller.enabled = true;
        }
        if (big)
        {
            big.controller.enabled = false;
            big.transform.position = Global.saveScene.big.position;
            big.transform.rotation = Global.saveScene.big.rotation;
            big.controller.enabled = true;
        }
    }

    public void SaveCheckpoint(Checkpoint checkpoint)
    {
        SaveData data = new();
        data.sceneData = new();
        data.achievDava = Global.achievment;

        data.sceneData.level = level;
        data.sceneData.checkpoint = checkpointIndex;
        data.sceneData.small = new RobotDataAdapter(checkpoint.smallPosition);
        data.sceneData.big = new RobotDataAdapter(checkpoint.bigPosition);

        SaveManager.Save(data);
    }

    public void LoadGame()
    {
        SaveData data = SaveManager.Load();

        Global.saveScene = data.sceneData;

        SceneManager.LoadScene(level);
        
    }

}
