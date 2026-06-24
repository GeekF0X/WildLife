using UnityEngine;

public class animais : MonoBehaviour
{
    public Animator animator;
    public AudioSource som;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            animator.SetBool("entrou", true);
        }
    }

    public void vozanimal()
    {
        som.Stop();
        som.Play();
        animator.SetBool("entrou", false);
    }
    
}
