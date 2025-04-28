using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;

    public void Die()
    {
        Destroy(gameObject);
    }

    public void TakeDamage(int damage)
    {
        maxHealth -= damage;
        if (maxHealth <= 0)
        {
            Die();
        }
    }

}

