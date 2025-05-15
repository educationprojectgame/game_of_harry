using Unity.VisualScripting;
using UnityEngine;

public class MonsterGetGamageInPlayerInvVcity : MonoBehaviour
{
    public int damage = 10;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnCollisionEnter2D(Collision2D collision)
    {
        HealthPlayer health = collision.gameObject.GetComponent<HealthPlayer>();

        if (collision.gameObject.CompareTag("MonsterBlue") || collision.gameObject.CompareTag("micropenis"))
        {
            HealthPlayer healthone = GetComponent<HealthPlayer>(); // получаем своё здоровье
            if (healthone != null)
            {
                healthone.TakeDamage(10); // допустим, каждая пуля наносит 10 урона
            }
        }

    }
}
