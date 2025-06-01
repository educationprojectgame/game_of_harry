using UnityEngine;
using UnityEngine.SceneManagement;

public class ChessFieldTrigger3 : MonoBehaviour
{
    public string sceneToLoad = "Chess3"; // ��� ����� �� Build Settings

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("PlayerGame"))
        {
            Debug.Log("����� �������� �� ������. ��������� ����� " + sceneToLoad);
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
