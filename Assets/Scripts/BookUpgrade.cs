using UnityEngine;

public class BookUpgrade : MonoBehaviour
{
    [Header("Статы для улучшения")]
    public float healthIncrease = 10f;    // Сколько добавить HP
    public float manaIncrease = 15f;     // Сколько добавить маны
    public float speedIncrease = 1f;     // Сколько добавить скорости

    private bool isUpgraded = false;     // Флаг, было ли улучшение
    private GameObject player;          // Ссылка на игрока

    public GameObject chest;

    void Start()
    {
        if (PlayerPrefs.GetInt("ChestIsOpened") == 1)
            chest.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Объект вошёл в триггер: " + other.name + " с тегом: " + other.tag);
        // Проверяем, что вошёл игрок
        if (other.CompareTag("PlayerGame"))
        {
            player = other.gameObject;
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Игрок вышел из зоны
        if (other.CompareTag("PlayerGame"))
        {
            player = null;
        }
    }

    void Update()
    {
        // Если игрок рядом и нажал E
        if (player != null && Input.GetKeyDown(KeyCode.E) && !isUpgraded)
        {
            UpgradePlayerStats();
            isUpgraded = true;  // Запрещаем повторное улучшение
            chest.SetActive(false);
            PlayerPrefs.SetInt("ChestIsOpened", 1);
            Debug.Log("Статы улучшены!");
        }
    }

    void UpgradePlayerStats()
    {
        // Получаем скрипты игрока
        var health = player.GetComponent<HealthPlayer>();
        var playerStats = player.GetComponent<Player>();

        // Увеличиваем статы (проверяем, чтобы скрипты были)
        if (health != null)
        {
            health.HealthPlay += healthIncrease;
        }

        if (playerStats != null)
        {
            playerStats.IncreaseMana(manaIncrease);
            PlayerPrefs.SetFloat("Mana", PlayerPrefs.GetFloat("Mana") + manaIncrease);
            playerStats.IncreaseSpeed(speedIncrease);
            PlayerPrefs.SetFloat("Speed", PlayerPrefs.GetFloat("Speed") + speedIncrease);
        }
    }
}