using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealthStoneNPC : MonoBehaviour
{
    public float maxHealth = 30;
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
