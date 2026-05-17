using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MixerControler : MonoBehaviour
{
    public AudioMixer mixer;

    public Slider sldMusic;
    public Slider sldEffects;
    public Slider sldGeral;


    private void Start()
    {
        sldMusic.value = PlayerPrefs.GetFloat("Music", 1f);
        sldEffects.value = PlayerPrefs.GetFloat("Effects", 1f);
        sldGeral.value = PlayerPrefs.GetFloat("Geral", 1f);

        ChangeMusicVolume();
        ChangeEffectsVolume();
        ChangeGeralVolume();
    }

    public void ChangeMusicVolume()
    {

        float volume = sldMusic.value;

        SetVolume("Music", volume);

        PlayerPrefs.SetFloat("Music", volume);
    }

    public void ChangeEffectsVolume()
    {
        float volume = sldEffects.value;

        SetVolume("Effects", volume);

        PlayerPrefs.SetFloat("Effects", volume);
    }

    public void ChangeGeralVolume()
    {
        float volume = sldGeral.value;

        SetVolume("Geral", volume);

        PlayerPrefs.SetFloat("Geral", volume);
    }

   private void SetVolume(string parameter, float volume)
    {
        if (volume <= 0.0001f)
        {
            mixer.SetFloat(parameter, -80f);
        }
        else
        {
            mixer.SetFloat(parameter, Mathf.Log10(volume) * 20);
        }
    }
}
