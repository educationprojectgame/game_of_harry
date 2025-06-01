using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement; // ⬅ добавим для смены сцены

public class BlueTileQuest2D : MonoBehaviour
{
    public GameObject blueTilePrefab;
    public Transform player;
    public int gridSize = 8;
    public Vector2 boardOrigin = new Vector2(-3.5f, -3.5f);
    public float cellSize = 1f;
    public float timeToReach = 2.5f;
    public int totalRounds = 5;
    public string victorySceneName = "Castle"; // Название сцены для победы

    private GameObject currentBlueTile;
    private Vector2Int currentTarget;
    private int successCount = 0;

    void Start()
    {
        StartCoroutine(QuestRoutine());
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
            Debug.Log(" Успел встать на синюю клетку");
            successCount++;
        }
        else
        {
            Debug.Log(" Не успел");
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