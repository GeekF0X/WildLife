using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public GameObject puzzle;
    public Transform smallPosition, bigPosition;
    public bool condition = true;

    public DataManager manager;

    private void OnTriggerEnter(Collider other)
    {
        if ((other.tag == "Player") && condition)
        {
            Debug.Log("Passei");

            manager.checkpointIndex++;
            manager.SaveCheckpoint(this);
            Destroy(GetComponent<Collider>());
        }
    }

    public void SetCondition(bool value)
    {
        condition = value;
    }
}
