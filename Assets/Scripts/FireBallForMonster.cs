using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    public float speed = 10f;        // Скорость полета снаряда
    public float lifetime = 3f;      // Время жизни снаряда в секундах
    public int damage = 10;          // Урон от снаряда (если нужно)

    private Vector2 direction;

    void Start()
    {
        // Уничтожить снаряд через 'lifetime' секунд, если он никуда не попал
        Destroy(gameObject, lifetime);
    }

    // Этот метод будет вызываться из скрипта монстра для задания направления
    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;
    }

    void Update()
    {
        // Движение снаряда
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Проверяем, столкнулся ли снаряд с игроком
        if (other.gameObject.CompareTag("PlayerGame")) // Убедитесь, что у игрока есть тег "Player"
        {
            Debug.Log("Снаряд попал в игрока!");
            // Здесь можно добавить логику нанесения урона игроку
            HealthPlayer playerHealth = other.gameObject.GetComponent<HealthPlayer>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
            Destroy(gameObject); // Уничтожаем снаряд при попадании
        }
        // Можно добавить и другие проверки, например, на стены
        else if (other.gameObject.CompareTag("Ground")) // Пример: тег для земли/стен
        {
            Destroy(gameObject); // Уничтожаем снаряд
        }
    }
}