using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class TrajectoryFlow : MonoBehaviour
{
    [Tooltip("Velocidade de rolagem da textura. Negativo inverte o sentido.")]
    public float scrollSpeed = -2f;

    private LineRenderer lr;
    private Material     matInstance;  
    private Vector2      offset;

    void Awake()
    {
        lr = GetComponent<LineRenderer>();
        if (lr.sharedMaterial != null)
            matInstance = lr.material;
    }

    void Update()
    {
        if (matInstance == null || !lr.enabled) return;

        offset.x += scrollSpeed * Time.deltaTime;
        matInstance.mainTextureOffset = offset;
    }
}