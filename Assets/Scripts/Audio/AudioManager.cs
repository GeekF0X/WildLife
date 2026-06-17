using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Audio Sources")]
    [Tooltip("Source para sons de UI (2D, sem espacialização)")]
    public AudioSource uiSource;
    [Tooltip("Source para SFX globais 2D (game over, etc)")]
    public AudioSource sfxSource;
    [Tooltip("Source para música de fundo (loop)")]
    public AudioSource musicSource;

    [Header("UI Clips")]
    public AudioClip uiClick;
    public AudioClip uiHover;
    public AudioClip uiBack;

    [Header("Coletáveis")]
    public AudioClip collectiblePickup;

    [Header("Hazards")]
    public AudioClip shortCircuit;
    public AudioClip fallImpact;
    public AudioClip gameOver;

    [Header("Música")]
    public AudioClip backgroundMusic;
    [Range(0f, 1f)] public float musicVolume = 0.5f;

    [Header("Configurações")]
    [Range(0f, 1f)] public float masterVolume = 1f;
    [Range(0f, 1f)] public float sfxVolume = 1f;
    [Range(0f, 1f)] public float uiVolume = 1f;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        if (uiSource == null) uiSource = CreateSource("UI Source", false);
        if (sfxSource == null) sfxSource = CreateSource("SFX Source", false);
        if (musicSource == null) musicSource = CreateSource("Music Source", true);

        PlayMusic();
    }

    private AudioSource CreateSource(string name, bool loop)
    {
        var go = new GameObject(name);
        go.transform.SetParent(transform);
        var src = go.AddComponent<AudioSource>();
        src.playOnAwake = false;
        src.loop = loop;
        src.spatialBlend = 0f; // 2D
        return src;
    }

    public void PlayUIClick() => PlayUI(uiClick);
    public void PlayUIHover() => PlayUI(uiHover);
    public void PlayUIBack() => PlayUI(uiBack);

    private void PlayUI(AudioClip clip)
    {
        if (clip == null || uiSource == null) return;
        uiSource.PlayOneShot(clip, uiVolume * masterVolume);
    }

    public void PlaySFX(AudioClip clip, float volumeMultiplier = 1f)
    {
        if (clip == null || sfxSource == null) return;
        sfxSource.PlayOneShot(clip, sfxVolume * masterVolume * volumeMultiplier);
    }

    public void PlayCollectible() => PlaySFX(collectiblePickup);
    public void PlayShortCircuit() => PlaySFX(shortCircuit);
    public void PlayFallImpact() => PlaySFX(fallImpact);
    public void PlayGameOver() => PlaySFX(gameOver);

    public void PlaySFXAtPoint(AudioClip clip, Vector3 position, float volume = 1f)
    {
        if (clip == null) return;
        AudioSource.PlayClipAtPoint(clip, position, volume * sfxVolume * masterVolume);
    }

    public void PlayMusic()
    {
        if (backgroundMusic == null || musicSource == null) return;
        musicSource.clip = backgroundMusic;
        musicSource.volume = musicVolume * masterVolume;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void StopMusic()
    {
        if (musicSource != null) musicSource.Stop();
    }

    public void SetMusicVolume(float volume)
    {
        musicVolume = Mathf.Clamp01(volume);
        if (musicSource != null) musicSource.volume = musicVolume * masterVolume;
    }
}