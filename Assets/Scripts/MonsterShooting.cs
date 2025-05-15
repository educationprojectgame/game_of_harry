using UnityEngine;

public class MonsterShootingAI : MonoBehaviour
{
    public Transform playerTransform;     // Ссылка на Transform игрока
    public GameObject projectilePrefab;   // Префаб нашего снаряда

    public Transform firePoint;           // Точка, откуда будут вылетать снаряды (пустой объект-потомок монстра)
    public float fireRate = 0.5f;           // Количество выстрелов в секунду
    public float shootingRange = 10f;     // Дальность стрельбы
    public float projectileSpeed = 10f;   // Скорость снаряда (если вы хотите ее задавать отсюда)
    public float detectionHeightDifference = 1f; // Минимальная разница по высоте, чтобы считать, что игрок "снизу"

    private float nextFireTime = 0f;

    void Start()
    {
        // Попытка найти игрока по тегу, если не назначен вручную
        if (playerTransform == null)
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("PlayerGame");
            if (playerObject != null)
            {
                playerTransform = playerObject.transform;
            }
            else
            {
                Debug.LogError("Монстр не может найти игрока! Убедитесь, что у игрока есть тег 'Player' или назначьте его вручную.");
            }
        }

        if (firePoint == null)
        {
            Debug.LogError("Точка выстрела (Fire Point) не назначена для монстра!");
        }

        if (projectilePrefab == null)
        {
            Debug.LogError("Префаб снаряда (Projectile Prefab) не назначен для монстра!");
        }
    }

    void Update()
    {
        if (playerTransform == null || projectilePrefab == null || firePoint == null)
        {
            return; // Не стрелять, если что-то не настроено
        }

        // Проверка расстояния до игрока
        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);
        // Проверка, что игрок находится ниже монстра (с учетом detectionHeightDifference)
        //bool isPlayerBelow = playerTransform.position.y < (transform.position.y - detectionHeightDifference);

        if (distanceToPlayer <= shootingRange)
        {
            // Если пришло время для следующего выстрела
            if (Time.time >= nextFireTime)
            {
                Shoot();
                nextFireTime = Time.time + 1f / fireRate; // Обновляем время следующего выстрела
            }
        }
    }

    void Shoot()
    {
        // Создаем экземпляр снаряда в точке firePoint
        GameObject projectileGO = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        ProjectileScript projectileScript = projectileGO.GetComponent<ProjectileScript>();

        if (projectileScript != null)
        {
            // Вычисляем направление к игроку
            Vector2 directionToPlayer = (playerTransform.position - firePoint.position).normalized;
            projectileScript.SetDirection(directionToPlayer);
            projectileScript.speed = this.projectileSpeed; // Передаем скорость из монстра в снаряд
        }
        else
        {
            Debug.LogError("У префаба снаряда отсутствует ProjectileScript!");
        }
    }

    // Для визуализации радиуса стрельбы в редакторе
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, shootingRange);

        if (firePoint != null && playerTransform != null)
        {
            Gizmos.color = Color.yellow;
            // Линия до примерного уровня ниже монстра
            Vector3 belowPoint = new Vector3(transform.position.x, transform.position.y - detectionHeightDifference, transform.position.z);
            Gizmos.DrawLine(transform.position, belowPoint);
        }
    }
}