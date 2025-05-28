using UnityEngine;
using UnityEngine.SceneManagement;

public class ChessManager : MonoBehaviour
{
    public GameObject knightPrefab;
    public GameObject enemyPrefab;
    public GameObject keyPrefab;

    private GameObject knight;
    private Vector2Int playerPosition = new Vector2Int(0, 0);
    private float cellSize = 1f;
    private Vector2 boardOrigin = new Vector2(-3.5f, -3.5f);

    private Vector2Int keyPosition = new Vector2Int(7, 7);
    private Vector2Int[] enemyPositions = new Vector2Int[]
    {
        new Vector2Int(2, 3),
        new Vector2Int(4, 5),
        new Vector2Int(6, 2),
    };

    void Start()
    {
        // Спавним игрока
        knight = Instantiate(knightPrefab, PositionToWorld(playerPosition), Quaternion.identity);

        // Спавним ключ
        Instantiate(keyPrefab, PositionToWorld(keyPosition), Quaternion.identity);

        // Спавним врагов
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
        // Победа
        if (playerPosition == keyPosition)
        {
            Debug.Log("Победа! Гарри нашёл ключ!");


            TeleportData.returnPosition = new Vector2(35.63f, -12.34f);

            SceneManager.LoadScene("GameScene");
        }

        // Проигрыш
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