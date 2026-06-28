using UnityEngine;

public class CheckAchiev : MonoBehaviour
{
    public GameObject engrenagem, lampada, bateria, boneca, arma;
    void OnEnable()
    {
        var data = Global.achievment;
        if (data != null)
        {

            Check("engrenagem", engrenagem, data);
            Check("lampada", lampada, data);
            Check("bateria", bateria, data);
            Check("boneca", boneca, data);
            Check("Gun", arma, data);
        }
        else
        {
            engrenagem.SetActive(false);
            lampada.SetActive(false);
            bateria.SetActive(false);
            boneca.SetActive(false);
            arma.SetActive(false);
        }
    }

    void Check(string name, GameObject obj, AchievData data)
    {
        if (data.achievName.Contains(name))
        {
            int i = data.achievName.IndexOf(name);
            obj.SetActive(!data.achievValue[i]);
        }
        else
            obj.SetActive(false);
    }
}
