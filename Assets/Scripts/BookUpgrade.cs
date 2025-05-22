using UnityEngine;

public class BookUpgrade : MonoBehaviour
{
    [Header("����� ��� ���������")]
    public float healthIncrease = 10f;    // ������� �������� HP
    public float manaIncrease = 15f;     // ������� �������� ����
    public float speedIncrease = 1f;     // ������� �������� ��������

    private bool isUpgraded = false;     // ����, ���� �� ���������
    private GameObject player;          // ������ �� ������

    public GameObject chest;

    void Start()
    {
        if (PlayerPrefs.GetInt("ChestIsOpened") == 1)
            chest.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("������ ����� � �������: " + other.name + " � �����: " + other.tag);
        // ���������, ��� ����� �����
        if (other.CompareTag("PlayerGame"))
        {
            player = other.gameObject;
        }
    }

    void OnTriggerExit(Collider other)
    {
        // ����� ����� �� ����
        if (other.CompareTag("PlayerGame"))
        {
            player = null;
        }
    }

    void Update()
    {
        // ���� ����� ����� � ����� E
        if (player != null && Input.GetKeyDown(KeyCode.E) && !isUpgraded)
        {
            UpgradePlayerStats();
            isUpgraded = true;  // ��������� ��������� ���������
            chest.SetActive(false);
            PlayerPrefs.SetInt("ChestIsOpened", 1);
            Debug.Log("����� ��������!");
        }
    }

    void UpgradePlayerStats()
    {
        // �������� ������� ������
        var health = player.GetComponent<HealthPlayer>();
        var playerStats = player.GetComponent<Player>();

        // ����������� ����� (���������, ����� ������� ����)
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