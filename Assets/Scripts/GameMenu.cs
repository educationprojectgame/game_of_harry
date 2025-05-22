using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("ShowTab", 1);
        PlayerPrefs.SetFloat("Health", 100);
        PlayerPrefs.SetFloat("Mana", 100);
        PlayerPrefs.SetFloat("Speed", 5);
        PlayerPrefs.Save();
        // Заменить "GameScene" на точное имя твоей сцены
        SceneManager.LoadScene("House");
    }

    public void ExitGame()
    {
        Debug.Log("Выход из игры"); // На случай тестов в редакторе
        Application.Quit();
    }
}