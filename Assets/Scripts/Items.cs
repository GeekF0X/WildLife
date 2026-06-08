using NUnit.Framework;
using UnityEngine;

public class Items : MonoBehaviour
{
    public DataManager manager;
    private void Start()
    {
        int i = 0;
        foreach(string s in SaveManager.Load().achievDava.achievName)
        {
            if(s == this.gameObject.name)
            {
                this.gameObject.SetActive(false);
            }
        }   
    }
    private void OnTriggerEnter(Collider other)
    {
        manager.SaveItempego(this.gameObject);
    }
}
