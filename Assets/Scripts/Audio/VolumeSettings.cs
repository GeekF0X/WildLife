using UnityEngine;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [Header("Sliders")]
    public Slider masterSlider;
    public Slider musicSlider;
    public Slider sfxSlider;

    private const string KEY_MASTER = "vol_master";
    private const string KEY_MUSIC = "vol_music";
    private const string KEY_SFX = "vol_sfx";

    private void Start()
    {
        float master = PlayerPrefs.GetFloat(KEY_MASTER, 1f);
        float music = PlayerPrefs.GetFloat(KEY_MUSIC, 0.5f);
        float sfx = PlayerPrefs.GetFloat(KEY_SFX, 1f);

        if (masterSlider != null)
        {
            masterSlider.value = master;
            masterSlider.onValueChanged.AddListener(OnMasterChanged);
        }
        if (musicSlider != null)
        {
            musicSlider.value = music;
            musicSlider.onValueChanged.AddListener(OnMusicChanged);
        }
        if (sfxSlider != null)
        {
            sfxSlider.value = sfx;
            sfxSlider.onValueChanged.AddListener(OnSFXChanged);
        }

        ApplyVolumes(master, music, sfx);
    }

    private void OnMasterChanged(float value)
    {
        PlayerPrefs.SetFloat(KEY_MASTER, value);
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.masterVolume = value;
            AudioManager.Instance.SetMusicVolume(AudioManager.Instance.musicVolume);
        }
        Debug.Log($"[VolumeSettings] Volume Geral: {value:P0}");
    }

    private void OnMusicChanged(float value)
    {
        PlayerPrefs.SetFloat(KEY_MUSIC, value);
        if (AudioManager.Instance != null)
            AudioManager.Instance.SetMusicVolume(value);
        Debug.Log($"[VolumeSettings] Música: {value:P0}");
    }

    private void OnSFXChanged(float value)
    {
        PlayerPrefs.SetFloat(KEY_SFX, value);
        if (AudioManager.Instance != null)
            AudioManager.Instance.sfxVolume = value;
        Debug.Log($"[VolumeSettings] SFX: {value:P0}");
    }

    private void ApplyVolumes(float master, float music, float sfx)
    {
        if (AudioManager.Instance == null) return;
        AudioManager.Instance.masterVolume = master;
        AudioManager.Instance.SetMusicVolume(music);
        AudioManager.Instance.sfxVolume = sfx;
    }
}
