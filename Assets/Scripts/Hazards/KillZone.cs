using UnityEngine;

[RequireComponent(typeof(Collider))]
public class KillZone : MonoBehaviour
{
    private void Reset()
    {
        var col = GetComponent<Collider>();
        if (col != null) col.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (GameManager.Instance != null && GameManager.Instance.IsGameOver) return;

        Robot robot = other.GetComponentInParent<Robot>();
        if (robot == null) return;

        Debug.Log($"[KillZone] {robot.gameObject.name} caiu — fim de jogo");

        robot.gameObject.SetActive(false);

        if (GameManager.Instance != null)
            GameManager.Instance.TriggerGameOver(GameManager.FailReason.Fall);
    }
}