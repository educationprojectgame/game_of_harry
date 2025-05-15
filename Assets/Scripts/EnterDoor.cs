using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterDoor : MonoBehaviour
{
    private bool enterAllowed;
    private string spawnTarget;
    private string checkPointTarget;
    private string sceneToLoad;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<BlueDoor>())
        {
            spawnTarget = "BrownDoor";
            checkPointTarget = "BrownDoor";
            sceneToLoad = "Library";
        }
        else if (collision.GetComponent<BrownDoor>())
        {
            spawnTarget = "BlueDoor";
            sceneToLoad = "GameScene";
        }
        else if (collision.GetComponent<DoorToHagrid>())
        {
            spawnTarget = "HagridHouseDoor";
            checkPointTarget = "HagridHouseDoor";
            sceneToLoad = "Castle";
        }
        else if (collision.GetComponent<HagridHouseDoor>())
        {
            spawnTarget = "DoorToHagrid";
            sceneToLoad = "Library";
        }
        else if (collision.GetComponent<DoorToMonster>())
        {
            spawnTarget = "DoorBackFromMonster";
            checkPointTarget = "DoorBackFromMonster";
            sceneToLoad = "BossHouse";
        }
        else if (collision.GetComponent<DoorBackFromMonster>())
        {
            spawnTarget = "DoorToMonster";
            sceneToLoad = "Castle";
        }
        else if (collision.GetComponent<LadderToStone>())
        {
            spawnTarget = "LadderFromStone";
            sceneToLoad = "StoneRoom";
        }
        else if (collision.GetComponent<LadderFromStone>())
        {
            spawnTarget = "LadderToStone";
            sceneToLoad = "BossHouse";
        }


        PlayerPrefs.SetString("SpawnPoint", spawnTarget);
        PlayerPrefs.SetString("CheckPoint", checkPointTarget);
        PlayerPrefs.SetString("CheckPointScene", sceneToLoad);
        enterAllowed = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<BlueDoor>() || collision.GetComponent<BrownDoor>())
        {
            enterAllowed = false;
        }
    }

    private void Update()
    {
        if (enterAllowed && Input.GetKey(KeyCode.Return))
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
