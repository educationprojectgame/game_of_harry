using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterDoor : MonoBehaviour
{
    private string currentSpawnTarget;
    private string currentCheckPointTarget;
    private string currentSceneToLoad;
    private bool isInTrigger;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<BrownDoor>())
        {
            currentSpawnTarget = "BlueDoor";
            currentSceneToLoad = "GameScene";
            isInTrigger = true;
        }
        else if (collision.GetComponent<DoorToCastle>())
        {
            currentSpawnTarget = "HagridHouseDoor";
            currentCheckPointTarget = "HagridHouseDoor";
            currentSceneToLoad = "Castle";
            isInTrigger = true;
        }
        else if (collision.GetComponent<HagridHouseDoor>())
        {
            currentSpawnTarget = "DoorToCastle";
            currentSceneToLoad = "GameScene";
            isInTrigger = true;
        }
        else if (collision.GetComponent<DoorToMonster>())
        {
            currentSpawnTarget = "DoorBackFromMonster";
            currentCheckPointTarget = "DoorBackFromMonster";
            currentSceneToLoad = "BossHouse";
            isInTrigger = true;
        }
        else if (collision.GetComponent<DoorBackFromMonster>())
        {
            currentSpawnTarget = "DoorToMonster";
            currentSceneToLoad = "Castle";
            isInTrigger = true;
        }
        else if (collision.GetComponent<LadderToStone>())
        {
            currentSpawnTarget = "LadderFromStone";
            currentSceneToLoad = "StoneRoom";
            isInTrigger = true;
        }
        else if (collision.GetComponent<LadderFromStone>())
        {
            currentSpawnTarget = "LadderToStone";
            currentSceneToLoad = "BossHouse";
            isInTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<BlueDoor>() || collision.GetComponent<BrownDoor>() ||
            collision.GetComponent<DoorToCastle>() || collision.GetComponent<HagridHouseDoor>() ||
            collision.GetComponent<DoorToMonster>() || collision.GetComponent<DoorBackFromMonster>() ||
            collision.GetComponent<LadderToStone>() || collision.GetComponent<LadderFromStone>())
        {
            isInTrigger = false;
            // Очищаем текущие цели при выходе из триггера
            currentSpawnTarget = null;
            currentCheckPointTarget = null;
            currentSceneToLoad = null;
        }
    }

    private void Update()
    {
        if (isInTrigger && Input.GetKeyDown(KeyCode.Return) && !string.IsNullOrEmpty(currentSceneToLoad))
        {
            // Сохраняем данные только в момент нажатия Enter
            PlayerPrefs.SetString("SpawnPoint", currentSpawnTarget);

            if (!string.IsNullOrEmpty(currentCheckPointTarget))
            {
                PlayerPrefs.SetString("CheckPoint", currentCheckPointTarget);
                PlayerPrefs.SetString("CheckPointScene", currentSceneToLoad);
            }

            // Загружаем сцену
            SceneManager.LoadScene(currentSceneToLoad);
        }
    }
}