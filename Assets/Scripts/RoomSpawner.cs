using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public GameObject monsterPrefab;
    public Transform spawnPoint;

    void Start()
    {
        if (!GameStateManager.Instance.monsterDead)
        {
            Instantiate(monsterPrefab, spawnPoint.position, spawnPoint.rotation);
        }
    }
}