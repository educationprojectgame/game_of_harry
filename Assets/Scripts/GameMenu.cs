using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        // Заменить "GameScene" на точное имя твоей сцены
        SceneManager.LoadScene("GameScene");
    }

    public void ExitGame()
    {
        Debug.Log("Выход из игры"); // На случай тестов в редакторе
        Application.Quit();
    }
}