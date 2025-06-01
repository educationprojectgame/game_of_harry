using UnityEngine;
using UnityEngine.SceneManagement;

public class ChessFieldTrigger3 : MonoBehaviour
{
    public string sceneToLoad = "Игра 2"; // Имя сцены из Build Settings

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("PlayerGame"))
        {
            Debug.Log("Игрок наступил на клетку. Загружаем сцену " + sceneToLoad);
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
