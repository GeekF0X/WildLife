using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StoryPanels : MonoBehaviour
{
    public Image[] panels;
    public Button button;

    public float delay = 1f;
    public float fade = 0.8f;

    public AudioSource soundSource;
    public AudioClip thunder;
    public AudioSource loopSource;
    public AudioClip forest;

    void Start()
    {
        foreach (Image panel in panels)
        {
            Color c = panel.color;
            c.a = 0f;
            panel.color = c;
        }

        button.gameObject.SetActive(false);

        StartCoroutine(ShowPanels());
    }

    IEnumerator ShowPanels()
    {
        /*foreach (Image panel in panels)
        {
            yield return StartCoroutine(FadeIn(panel));

            yield return new WaitForSeconds(delay);
        }

        button.gameObject.SetActive(true);*/

        for (int i = 0; i < panels.Length; i++)
        {
            if (i == 1) soundSource.PlayOneShot(thunder);

            if(i == 2)
            {
                loopSource.clip = forest;
                loopSource.loop = true;
                loopSource.Play();
            }

            yield return StartCoroutine(FadeIn(panels[i]));

            yield return new WaitForSeconds(delay);
        }

        button.gameObject.SetActive(true);
    }

    IEnumerator FadeIn(Image panel)
    {
        Color c = panel.color;

        for (float i = 0; i < fade; i += Time.deltaTime)
        {
            c.a = Mathf.Lerp(0f, 1f, i / fade);
            panel.color = c;
            yield return null;
        }

        c.a = 1f;
        panel.color = c;
    }
}
