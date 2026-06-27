using UnityEngine;
using UnityEngine.VFX;

public class WaterfallEffects : MonoBehaviour
{
    public ParticleSystem Sprinkler;
    public VisualEffect WaterStop;

    public void StopFall()
    {
        Sprinkler.Stop();
        WaterStop.Play();
    }
    public void StartFall()
    {
        Sprinkler.Play();
        WaterStop.Stop();
    }
}
