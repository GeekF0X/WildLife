using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SaveData
{
    public SceneData sceneData;
    public AchievData achievDava;
}

[Serializable]
public class SceneData
{
    public RobotData small, big;
    public string level;
    public int checkpoint;
}

[Serializable]
public class RobotData
{
    public Vector3 position;
    public Quaternion rotation;
}

[Serializable]
public class AchievData
{
    public List<string> achievName;
    public List<bool> achievValue;
}