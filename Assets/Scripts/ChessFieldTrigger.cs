using UnityEngine;
using UnityEngine.SceneManagement;

public class ChessFieldTrigger : MonoBehaviour
{
    public string sceneToLoad = "Chess"; // Имя сцены из Build Settings

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Игрок наступил на клетку. Загружаем сцену " + sceneToLoad);
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
