using UnityEngine;

public interface IStates
{
    public void Enter();
    public void Update();
    public void Exit();

    public string GetName();
}
