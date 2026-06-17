using UnityEngine;


public class CollectibleSound : MonoBehaviour
{
    [Tooltip("Som customizado (opcional). Se vazio, usa o som padrão do AudioManager.")]
    public AudioClip customPickupSound;

    [Tooltip("Tempo até destruir o objeto após pegar (pra deixar o som tocar)")]
    public float destroyDelay = 0.1f;

    public void PlayPickupAndDestroy()
    {
        if (AudioManager.Instance != null)
        {
            if (customPickupSound != null)
                AudioManager.Instance.PlaySFXAtPoint(customPickupSound, transform.position);
            else
                AudioManager.Instance.PlayCollectible();
        }

        Destroy(gameObject, destroyDelay);
    }
}