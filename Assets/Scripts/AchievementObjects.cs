using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;

public class AchievementObjects : MonoBehaviour
{
    public DataManager manager;
    int i = 0;
    AchievData data;

    public DialogType firstDialog;
    public DialogType secondDialog;

    private void Start()
    {
        data = Global.achievment;
        if (data.achievName.Contains(this.gameObject.name))
        {
            this.i = data.achievName.IndexOf(this.gameObject.name);
            this.gameObject.SetActive(!data.achievValue[i]);
            Debug.Log(data.achievValue[this.i]);
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
            manager.SaveAchievment(this.i);

            if (DialogUI.Instance != null)
            {
                DialogUI.Instance.Show(firstDialog, secondDialog);
            }
        }
    }
}
