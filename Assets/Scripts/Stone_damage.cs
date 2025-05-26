using UnityEngine;

public class BossDamage : MonoBehaviour
{
    [SerializeField] private int damage = 10;
    [SerializeField] private float damageInterval = 1f; // интервал между ударами в секундах

    private float lastDamageTime;

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerGame"))
        {
            if (Time.time - lastDamageTime >= damageInterval)
            {
                HealthPlayer playerHealth = collision.gameObject.GetComponent<HealthPlayer>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(damage);
                    lastDamageTime = Time.time;
                }
            }
        }
    }
}