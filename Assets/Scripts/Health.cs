using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public string bossId = "UniqueBossID_Stone";
    public float maxHealth = 200;

    private float currentHealth;

    public Image Bar;

    public bool isMonster = false;

    void Awake()
    {
        // Проверяем, был ли этот босс уже побежден
        // Делаем это в Awake, чтобы босс исчез до того, как игрок его увидит
        if (ProgressManager.Instance != null && ProgressManager.Instance.IsBossDefeated(bossId))
        {
            Debug.Log($"Boss {bossId} was already defeated. Deactivating.");
            gameObject.SetActive(false); // Деактивируем босса, если он уже побежден
             // Выходим, чтобы не выполнять остальную логику инициализации
        }
        // Обычная инициализация, если босс не был побежден
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log($"Boss {bossId} took {damage} damage, current health: {currentHealth}");

        Bar.fillAmount = currentHealth / maxHealth;
        if (maxHealth <= 0 || currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        if (ProgressManager.Instance != null)
        {
            ProgressManager.Instance.MarkBossAsDefeated(bossId);
        }

        Destroy(gameObject);
    }
}