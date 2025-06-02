using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BlueTileQuest2D : MonoBehaviour
{
    public GameObject blueTilePrefab;
    public Transform player;
    public int gridSize = 8;
    public Vector2 boardOrigin = new Vector2(-3.5f, -3.5f);
    public float cellSize = 1f;
    public float timeToReach = 2.5f;
    public int totalRounds = 5;
    public string victorySceneName = "Игра 2";      // Сцена при победе
    public string exitSceneName = "Castle";         // Сцена при выходе по координате или клавише
    public Vector2Int exitCell = new Vector2Int(7, 0); // Координаты выхода

    private GameObject currentBlueTile;
    private Vector2Int currentTarget;
    private int successCount = 0;

    void Start()
    {
        StartCoroutine(QuestRoutine());
    }

    void Update()
    {
        // Выход или переход при нажатии на клавишу E
        if (Input.GetKeyDown(KeyCode.E))
        {
            string currentScene = SceneManager.GetActiveScene().name;

            if (currentScene == "Игра 2")
            {
                Debug.Log("Нажата E на сцене 'Игра 2'. Переход на сцену Castle.");
                SceneManager.LoadScene(exitSceneName);
            }
            else
            {
                Debug.Log("Выход из игры (не в сцене 'Игра 2')");
                Application.Quit();
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#endif
            }
        }

        // Проверка выхода по координате
        CheckExitCell();
    }

    IEnumerator QuestRoutine()
    {
        for (int i = 0; i < totalRounds; i++)
        {
            SpawnBlueTile();
            yield return new WaitForSeconds(timeToReach);
            CheckPlayer();
            RemoveBlueTile();
            yield return new WaitForSeconds(0.5f);

            if (successCount >= 2)
            {
                Debug.Log("Достигнуто 2 попадания! Переход на сцену победы.");
                SceneManager.LoadScene(victorySceneName);
                yield break;
            }
        }

        Debug.Log("Квест завершён");
    }

    void SpawnBlueTile()
    {
        int x = Random.Range(0, gridSize);
        int y = Random.Range(0, gridSize);
        currentTarget = new Vector2Int(x, y);
        Vector2 spawnPos = boardOrigin + new Vector2(x * cellSize, y * cellSize);

        currentBlueTile = Instantiate(blueTilePrefab, spawnPos, Quaternion.identity);
    }

    void RemoveBlueTile()
    {
        if (currentBlueTile)
            Destroy(currentBlueTile);
    }

    void CheckPlayer()
    {
        Vector2Int playerCell = WorldToCell(player.position);
        if (playerCell == currentTarget)
        {
            Debug.Log("Успел встать на синюю клетку");
            successCount++;
        }
        else
        {
            Debug.Log("Не успел");
        }
    }

    void CheckExitCell()
    {
        Vector2Int playerCell = WorldToCell(player.position);
        if (playerCell == exitCell)
        {
            Debug.Log("Игрок встал на клетку выхода! Загружается сцена: " + exitSceneName);
            SceneManager.LoadScene(exitSceneName);
        }
    }

    Vector2Int WorldToCell(Vector2 worldPos)
    {
        Vector2 localPos = worldPos - boardOrigin;
        int x = Mathf.FloorToInt(localPos.x / cellSize);
        int y = Mathf.FloorToInt(localPos.y / cellSize);
        return new Vector2Int(x, y);
    }
}
