using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;

    public void TakeDamage(int damage)
    {
        maxHealth -= damage;
        if (maxHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}