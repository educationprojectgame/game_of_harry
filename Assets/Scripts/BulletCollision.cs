using UnityEngine;

public class BulletCollision : MonoBehaviour
{
    private void Start()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Опционально: проверяем тег, чтобы не уничтожаться от "своих"
        if (!other.CompareTag("PlayerGame") && !other.CompareTag("WandCollider"))
        {
            Destroy(gameObject);
        }
    }
}