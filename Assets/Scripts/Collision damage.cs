using UnityEngine;

public class CollisionDamage : MonoBehaviour
{
    public int damage = 10;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Health health = collision.gameObject.GetComponent<Health>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            Health health = GetComponent<Health>(); // получаем своё здоровье
            if (health != null)
            {
                health.TakeDamage(10); // допустим, каждая пуля наносит 10 урона
                Destroy(collision.gameObject); // уничтожаем пулю при попадании
            }
        }
    }
}