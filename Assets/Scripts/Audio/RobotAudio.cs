using UnityEngine;


[RequireComponent(typeof(AudioSource))]
public class RobotAudio : MonoBehaviour
{
    [Header("Audio Source")]
    public AudioSource sfxSource;
    public AudioSource movementSource; 

    [Header("Clips")]
    public AudioClip movementLoop;
    public AudioClip hookShot;
    public AudioClip hookAttach;
    public AudioClip throwObject;

    [Header("Configurações")]
    [Range(0f, 1f)] public float volume = 1f;
    [Tooltip("Variação aleatória de pitch (0 = sem variação)")]
    [Range(0f, 0.3f)] public float pitchVariation = 0.05f;

    private void Awake()
    {
        if (sfxSource == null) sfxSource = GetComponent<AudioSource>();
        sfxSource.playOnAwake = false;
        sfxSource.spatialBlend = 1f; // 3D

        if (movementSource == null)
        {
            var go = new GameObject("Movement Audio");
            go.transform.SetParent(transform);
            go.transform.localPosition = Vector3.zero;
            movementSource = go.AddComponent<AudioSource>();
        }

        movementSource.playOnAwake = false;
        movementSource.loop = true;
        movementSource.spatialBlend = 1f;
        movementSource.volume = volume * 0.6f;
        if (movementLoop != null) movementSource.clip = movementLoop;
    }

      public void StartMovementSound()
    {
        if (movementSource != null && movementLoop != null && !movementSource.isPlaying)
            movementSource.Play();
    }

    public void StopMovementSound()
    {
        if (movementSource != null && movementSource.isPlaying)
            movementSource.Stop();
    }

    public void PlayHookShot() => PlayOneShotVaried(hookShot);

    public void PlayHookAttach() => PlayOneShotVaried(hookAttach);

    public void PlayThrow() => PlayOneShotVaried(throwObject);

    private void PlayOneShotVaried(AudioClip clip)
    {
        if (clip == null || sfxSource == null) return;

        sfxSource.pitch = 1f + Random.Range(-pitchVariation, pitchVariation);
        sfxSource.PlayOneShot(clip, volume);
        sfxSource.pitch = 1f;
    }
}