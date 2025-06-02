using UnityEngine;
using UnityEngine.SceneManagement;

public class ChessManager1 : MonoBehaviour
{
    public GameObject knightPrefab;
    public GameObject enemyPrefab;
    public GameObject keyPrefab;

    private GameObject knight;
    private Vector2Int playerPosition = new Vector2Int(2, 2);
    private float cellSize = 1f;
    private Vector2 boardOrigin = new Vector2(-3.5f, -3.5f);

    private Vector2Int keyPosition = new Vector2Int(5, 5);
    private Vector2Int exitCell = new Vector2Int(-70, -9); // Координата, на которой можно выйти по E

    private Vector2Int[] enemyPositions = new Vector2Int[]
    {
        new Vector2Int(3, 4),
        new Vector2Int(6, 7),
        new Vector2Int(4, 1),
        new Vector2Int(1, 1),
    };

    void Start()
    {
        knight = Instantiate(knightPrefab, PositionToWorld(playerPosition), Quaternion.identity);
        Instantiate(keyPrefab, PositionToWorld(keyPosition), Quaternion.identity);

        foreach (var pos in enemyPositions)
        {
            Instantiate(enemyPrefab, PositionToWorld(pos), Quaternion.identity);
        }
    }

    void Update()
    {
        Vector2Int[] moves = {
            new Vector2Int(1, 2), new Vector2Int(2, 1),
            new Vector2Int(-1, 2), new Vector2Int(-2, 1),
            new Vector2Int(1, -2), new Vector2Int(2, -1),
            new Vector2Int(-1, -2), new Vector2Int(-2, -1)
        };

        for (int i = 0; i < moves.Length; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                Vector2Int targetPos = playerPosition + moves[i];
                if (IsInsideBoard(targetPos))
                {
                    playerPosition = targetPos;
                    knight.transform.position = PositionToWorld(playerPosition);
                    CheckCollision();
                }
            }
        }

        // Проверка выхода по клавише E
        if (Input.GetKeyDown(KeyCode.E) && playerPosition == exitCell)
        {
            Debug.Log("Гарри покидает уровень через координату выхода!");
            TeleportData.returnPosition = new Vector2(-70f, -9f); // Устанавливаем позицию возврата
            SceneManager.LoadScene("GameScene"); // Загружаем нужную сцену
        }
    }

    Vector3 PositionToWorld(Vector2Int pos)
    {
        return new Vector3(boardOrigin.x + pos.x * cellSize, boardOrigin.y + pos.y * cellSize, -1);
    }

    bool IsInsideBoard(Vector2Int pos)
    {
        return pos.x >= 0 && pos.x < 8 && pos.y >= 0 && pos.y < 8;
    }

    void CheckCollision()
    {
        if (playerPosition == keyPosition)
        {
            Debug.Log("Победа! Гарри нашёл ключ!");

            PlayerPrefs.SetFloat("Mana", PlayerPrefs.GetFloat("Mana") + 5);
            PlayerPrefs.SetFloat("Speed", PlayerPrefs.GetFloat("Speed") + 0.5f);

            TeleportData.returnPosition = new Vector2(-70f, -9f);

            SceneManager.LoadScene("GameScene");
        }

        foreach (var enemy in enemyPositions)
        {
            if (playerPosition == enemy)
            {
                Debug.Log("Гарри пойман врагом! Игра окончена.");
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
}