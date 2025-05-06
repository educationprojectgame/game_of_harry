using UnityEngine;
using UnityEngine.UI;

public class HealthPlayer : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float maxHealth = 100f;
    public Image Bar;

    public void TakeDamage(int damage)
    {
        maxHealth -= damage;
        Bar.fillAmount = maxHealth / 100;
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
