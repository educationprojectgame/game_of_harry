using UnityEngine;
using UnityEngine.UI;

public class TabImageController : MonoBehaviour
{
    public KeyCode toggleKey = KeyCode.Tab; // Клавиша для управления
    public Image targetImage; // Ссылка на компонент Image
    public string imagePath = "Education"; // Путь к изображению в Resources (без расширения)

    void Start()
    {
        // Загружаем спрайт из ресурсов
        Sprite newSprite = Resources.Load<Sprite>(imagePath);

        // Проверяем, что спрайт успешно загружен
        if (newSprite != null && targetImage != null)
        {
            targetImage.sprite = newSprite; // Устанавливаем спрайт в компонент Image
            if (PlayerPrefs.GetInt("ShowTab") == 1)
            {
                targetImage.enabled = true; // Открываем изображение при старте
                PlayerPrefs.SetInt("ShowTab", 0);
            }

            else
                targetImage.enabled = false;
        }
        else
        {
            Debug.LogError("Не удалось загрузить спрайт или компонент Image не задан.");
        }
    }

    void Update()
    {
        if (targetImage == null) return;

        // Включаем изображение при нажатии
        if (Input.GetKeyDown(toggleKey))
        {
            targetImage.enabled = true;
        }

        // Выключаем при отпускании
        if (Input.GetKeyUp(toggleKey))
        {
            targetImage.enabled = false;
        }
    }
}