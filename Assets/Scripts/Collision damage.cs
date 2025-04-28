using UnityEngine;

public class CollisionDamage : MonoBehaviour
{
    public int damage = 10;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Health health = collision.gameObject.GetComponent<Health>();

        if (health != null)
        {
            health.TakeDamage(damage);
        }
    }
}
