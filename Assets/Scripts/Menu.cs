using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void PlayGame()
    {
        // Заменить "GameScene" на точное имя твоей сцены
        SceneManager.LoadScene("Дом наставника");
    }

    public void ExitGame()
    {
        Debug.Log("Выход из игры"); // На случай тестов в редакторе
        Application.Quit();
    }
}