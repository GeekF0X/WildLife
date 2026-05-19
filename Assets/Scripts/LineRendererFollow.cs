using UnityEngine;

public class LineRenderFollow : MonoBehaviour
{
    public LineRenderer lineRenderer;
    [Header("Start Anchor")]
    public Transform startAnchor;
    public Vector3 startOffset;

    [Header("End Anchor")]
    public Transform endAnchor;
    public Vector3 endOffset;

    void Update()
    {
        if (startAnchor != null && endAnchor != null)
        {
            lineRenderer.SetPosition(0, startAnchor.position + startOffset);
            lineRenderer.SetPosition(1, endAnchor.position + endOffset);
        }
    }
}