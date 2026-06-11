using UnityEngine;

public class efeitotroca : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public Transform Pfinal;
    public int divisoesdoline = 10;
    public float alturamax=0;
    Vector3 inicio, fim;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lineRenderer.positionCount = divisoesdoline;
        inicio = transform.position*2;
        fim = Pfinal.position*2;
    }

    // Update is called once per frame
    void Update()
    {
        if (lineRenderer.enabled  && (this.transform.position != inicio || Pfinal.position != fim))
        {
            inicio = this.transform.position;
            fim = Pfinal.position;

            for (int i = 0; i < divisoesdoline; i++)
            {
                float t = i / (float)(divisoesdoline - 1);
                Debug.Log(t+"  "+ i);
                Vector3 pontoLinear = Vector3.Lerp(inicio, fim, t);
                float altura = 4 * alturamax * t * (1 - t);
                Vector3 pontoFinal = pontoLinear + Vector3.up * altura;
                lineRenderer.SetPosition(i, pontoFinal);
            }
        }
    }
}
