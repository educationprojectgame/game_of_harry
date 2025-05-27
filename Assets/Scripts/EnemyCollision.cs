using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    [Header("Настройки взрыва")]
    public Animator animator;
    public string explodeTrigger = "explode52";

    [Header("Звук")]
    public AudioClip explosionSound;
    [Range(0, 1)] public float volume = 1f;

    [Header("Настройки")]
    public string playerTag = "Player";
    public GameObject destroyEffect;
    public int damage = 20;

    private bool isExploded = false;
    private AudioSource audioSource;

    private void Start()
    {
        // Создаем и настраиваем AudioSource
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.volume = volume;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isExploded && collision.gameObject.CompareTag(playerTag))
        {
            isExploded = true;
            if (collision.gameObject.TryGetComponent(out HealthPlayer health))
            {
                health.TakeDamage(damage);
            }
            StartExplosion();
        }
    }

    private void StartExplosion()
    {
        DisableComponents();

        // Анимация
        if (animator != null)
        {
            animator.SetTrigger(explodeTrigger);
        }

        // Визуальный эффект
        if (destroyEffect != null)
        {
            Instantiate(destroyEffect, transform.position, Quaternion.identity);
        }

        // Звук взрыва
        if (explosionSound != null)
        {
            // Вариант 1: Через существующий AudioSource
            AudioSource.PlayClipAtPoint(explosionSound, transform.position, volume);

            // Вариант 2: Независимое воспроизведение
            // AudioSource.PlayClipAtPoint(explosionSound, transform.position, volume);
        }
        else
        {
            Debug.LogWarning("Звук взрыва не назначен!");
        }

        Destroy(gameObject);
    }

    private void DisableComponents()
    {
        GetComponent<Collider2D>().enabled = false;
        if (TryGetComponent(out Rigidbody2D rb)) rb.simulated = false;
    }
}