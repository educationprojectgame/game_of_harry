using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    [Header("Настройки взрыва")]
    public Animator animator;
    public string explodeTrigger = "explode52";
    private bool isExploded = false;

    [Header("Настройки")]
    public string playerTag = "PlayerGame";
    public GameObject destroyEffect; // опционально — можно не использовать
    public int damage = 20;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isExploded && collision.gameObject.CompareTag(playerTag))
        {
            isExploded = true;

            // Наносим урон игроку
            HealthPlayer health = collision.gameObject.GetComponent<HealthPlayer>();
            if (health != null)
                health.TakeDamage(damage);

            StartExplosion();
        }
    }

    private void StartExplosion()
    {
        DisableComponents();

        // Запускаем взрывную анимацию
        if (animator != null)
        {
            animator.SetTrigger(explodeTrigger);
            Debug.Log("Анимация взрыва запущена!");
        }

        // Опционально: создаем дополнительный эффект (например, частицы)
        if (destroyEffect != null)
        {
            Instantiate(destroyEffect, transform.position, Quaternion.identity);
        }

        // Уничтожаем объект после задержки
        Destroy(gameObject);
    }

    private void DisableComponents()
    {
        // Отключаем физику и коллайдеры
        foreach (var col in GetComponents<Collider2D>())
            col.enabled = false;

        var rb = GetComponent<Rigidbody2D>();
        if (rb != null)
            rb.simulated = false;

        // НЕ отключаем SpriteRenderer — чтобы анимация отыграла
    }
}
