using System.Collections;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public Transform destinationPortal;
    public float teleportCooldown = 0.5f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Проверяем, что объект — игрок и он может телепортироваться
        if (other.CompareTag("Player"))
        {
            PlayerTeleportTracker tracker = other.GetComponent<PlayerTeleportTracker>();

            if (tracker != null && tracker.canTeleport)
            {
                StartCoroutine(Teleport(other, tracker));
            }
        }
    }

    private IEnumerator Teleport(Collider2D player, PlayerTeleportTracker tracker)
    {
        // Запрещаем повторную телепортацию
        tracker.canTeleport = false;

        // Телепортируем игрока
        player.transform.position = destinationPortal.position;

        // Ждём кулдаун
        yield return new WaitForSeconds(teleportCooldown);

        // Разрешаем телепортироваться снова
        tracker.canTeleport = true;
    }
}
