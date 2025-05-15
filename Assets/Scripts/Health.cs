using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public float maxHealth = 1000;
    public Image Bar;

    public void TakeDamage(int damage)
    {
        maxHealth -= damage;
        Bar.fillAmount = maxHealth / 1000;
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