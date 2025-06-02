using UnityEngine;
using UnityEngine.SceneManagement; // Необходимо для управления сценами

public class SceneChanger : MonoBehaviour
{
    // Имя сцены, на которую нужно перейти.
    // Вы можете установить это значение в инспекторе Unity.
    public string sceneNameToLoad;

    // Update вызывается один раз за кадр
    void Update()
    {
        // Проверяем, нажата ли клавиша 'E'
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Загружаем указанную сцену
            LoadScene();
        }
    }

    void LoadScene()
    {
        // Убедитесь, что имя сцены указано
        if (!string.IsNullOrEmpty(sceneNameToLoad))
        {
            SceneManager.LoadScene(sceneNameToLoad);
        }
        else
        {
            Debug.LogError("Имя сцены для загрузки не указано!");
        }
    }
}