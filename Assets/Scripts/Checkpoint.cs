using UnityEngine;

public class Checkpoint : MonoBehaviour
{
#nullable enable
    public GameObject? puzzle;
    public Transform smallPosition, bigPosition;
    public bool condition = true;

    public int selfIndex;

    public DataManager manager;

    private void OnTriggerEnter(Collider other)
    {
        if ((other.tag == "Player") && condition)
        {
            Debug.Log("Passei");

            manager.checkpointIndex = selfIndex;
            manager.SaveCheckpoint(this);
            Destroy(GetComponentInChildren<Collider>());
        }
    }

    public void SetCondition(bool value)
    {
        condition = value;
    }
}
