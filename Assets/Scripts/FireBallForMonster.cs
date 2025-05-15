using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    public float speed = 10f;       // Скорость полета снаряда
    public float lifetime = 0.2f;       // Время жизни снаряда в секундах
    public int damage = 10;           // Урон от снаряда (если нужно)
    public GameObject hitEffectPrefab; // Ссылка на префаб эффекта попадания/исчезновения

    private Vector2 direction;
    private bool hasHit = false; // Флаг, чтобы эффект создавался только один раз

    void Start()
    {
        // Уничтожить снаряд через 'lifetime' секунд, если он никуда не попал
        // и вызвать эффект исчезновения
        Invoke("OnLifetimeEnd", lifetime);
    }

    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;
    }

    void Update()
    {
        if (hasHit) return; // Если уже попали, не двигаемся

        // Движение снаряда
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (hasHit) return; // Если уже обработали попадание, выходим

        // Проверяем, столкнулся ли снаряд с игроком
        if (other.gameObject.CompareTag("PlayerGame")) // Убедитесь, что у игрока есть тег "PlayerGame"
        {
            Debug.Log("Снаряд попал в игрока!");
            HealthPlayer playerHealth = other.gameObject.GetComponent<HealthPlayer>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
            HandleHit();
        }
        // Можно добавить и другие проверки, например, на стены
        else if (other.gameObject.CompareTag("trees")) // Пример: тег для земли/стен
        {
            Debug.Log("Снаряд попал в препятствие!");
            HandleHit();
        }
    }

    // Метод, который будет вызван по истечении времени жизни снаряда
    void OnLifetimeEnd()
    {
        if (!hasHit) // Если снаряд не попал ни во что до истечения времени жизни
        {
            Debug.Log("Снаряд исчез по истечении времени жизни.");
            HandleHit(); // Можно также использовать этот метод для создания эффекта "исчезновения"
        }
    }

    // Общий метод для обработки попадания или исчезновения
    void HandleHit()
    {
        hasHit = true; // Устанавливаем флаг, что попадание обработано

        // Создаем эффект попадания/исчезновения, если он назначен
        if (hitEffectPrefab != null)
        {
            Instantiate(hitEffectPrefab, transform.position, Quaternion.identity);
        }

        // Уничтожаем сам снаряд
        Destroy(gameObject);
    }
}