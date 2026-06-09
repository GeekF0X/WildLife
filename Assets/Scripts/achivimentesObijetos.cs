using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;

public class achivimentesObijetos : MonoBehaviour
{
    public DataManager manager;
    int i = 0;
    SaveData data;

    
    private void Start()
    {
        data = SaveManager.Load();
        if (data.achievDava.achievName.Contains(this.gameObject.name))
        {
            this.i = data.achievDava.achievName.IndexOf(this.gameObject.name);
            this.gameObject.SetActive(!data.achievDava.achievValue[i]);
            Debug.Log(data.achievDava.achievValue[this.i]);
        }
        else
        {
            manager.AddachiveList(this.gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log(this.i + "  " + "pegouI");
            manager.concluiuachive(this.i);
        }
    }
}
