using UnityEngine;

public class WaterHazard : MonoBehaviour
{
    public GameObject shortCircuitVFX;
    public bool isActive = true;

    public void HandleTrigger(Collider other)
    {
        if (!isActive) return;
        if (GameManager.Instance != null && GameManager.Instance.IsGameOver) return;

        Robot robot = other.GetComponentInParent<Robot>();
        if (robot == null) return;
        if (!robot.isEnergized) return;

        Debug.Log($"[WaterHazard] {robot.gameObject.name} entrou na água energizado — CURTO!");

        if (shortCircuitVFX != null)
            Instantiate(shortCircuitVFX, robot.transform.position, Quaternion.identity);

        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayShortCircuit();
        robot.gameObject.SetActive(false);

        if (GameManager.Instance != null)
            GameManager.Instance.TriggerGameOver(GameManager.FailReason.Water);
    }

    private void OnTriggerEnter(Collider other) => HandleTrigger(other);

    public void StopWater()
    {
        isActive = false;
        Collider[] allColliders = GetComponentsInChildren<Collider>();
        foreach (var col in allColliders)
            col.enabled = false;

        Debug.Log("[WaterHazard] Água parada — colliders desativados.");
    }

    public void StartWater()
    {
        isActive = true;
        Collider[] allColliders = GetComponentsInChildren<Collider>();
        foreach (var col in allColliders)
            col.enabled = true;
    }
}