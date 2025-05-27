using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    // Уникальный ID для этого кристалла. Установите его в Инспекторе.
    // Например: "RedCrystal_Cave", "BlueCrystal_Shrine"
    public string itemId;
    public bool destroyOnCollect = true; // Уничтожить объект после сбора?

    // Можно добавить визуальные/звуковые эффекты
    // public GameObject collectEffectPrefab;
    // public AudioClip collectSound;

    private bool isCollected = false;

    void Start()
    {
        // Если предмет уже был собран ранее (например, при перезагрузке сцены),
        // и он должен быть уничтожен, деактивируем его сразу.
        if (PManager.Instance != null && PManager.Instance.IsItemCollected(itemId))
        {
            if (destroyOnCollect)
            {
                gameObject.SetActive(false);
                isCollected = true; // Отмечаем как собранный, чтобы избежать повторной логики
            }
            // Здесь можно добавить логику, если предмет не уничтожается,
            // а, например, меняет свой вид.
        }
    }

    // Обычно сбор происходит при столкновении с игроком
    void OnTriggerEnter(Collider other) // Или OnTriggerEnter2D для 2D
    {
        if (isCollected) return; // Уже собран

        // Проверяем, что это игрок (используйте тэг "Player" или компонент)
        if (other.CompareTag("PlayerGame"))
        {
            Collect();
        }
    }

    void Collect()
    {
        if (isCollected) return;

        Debug.Log($"Player collected item: {itemId}");
        isCollected = true;

        // Сообщаем ProgressManager, что предмет собран
        if (PManager.Instance != null)
        {
            PManager.Instance.MarkItemAsCollected(itemId);
        }
        else
        {
            Debug.LogError("ProgressManager instance not found!");
            // В этом случае состояние не сохранится между сценами, но предмет исчезнет локально
        }

        // (Опционально) Проиграть эффект, звук
        // if (collectEffectPrefab != null) Instantiate(collectEffectPrefab, transform.position, Quaternion.identity);
        // if (collectSound != null) AudioSource.PlayClipAtPoint(collectSound, transform.position);

        if (destroyOnCollect)
        {
            Destroy(gameObject);
        }
        else
        {
            // Если не уничтожаем, можно, например, изменить его вид
            // GetComponent<Renderer>().material.color = Color.gray; // Пример
            // Или деактивировать только коллайдер
            // GetComponent<Collider>().enabled = false;
        }
    }
}