using UnityEngine;

public class MapToggle : MonoBehaviour
{
    public Camera mainCamera;    // Основная камера
    public Camera mapCamera;    // Камера карты
    public Canvas HpManaBar;

    void Update()
    {
        // Включаем камеру карты при зажатии Q
        if (Input.GetKeyDown(KeyCode.Q))
        {
            mainCamera.gameObject.SetActive(false);
            mapCamera.gameObject.SetActive(true);
            HpManaBar.gameObject.SetActive(false);
        }
        // Возвращаем основную камеру при отпускании Q
        else if (Input.GetKeyUp(KeyCode.Q))
        {
            mapCamera.gameObject.SetActive(false);
            mainCamera.gameObject.SetActive(true);
            HpManaBar.gameObject.SetActive(true);
        }
    }
}