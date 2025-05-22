using UnityEngine;

public class MapToggle : MonoBehaviour
{
    public Camera mainCamera;    // �������� ������
    public Camera mapCamera;    // ������ �����
    public Canvas HpManaBar;

    void Update()
    {
        // �������� ������ ����� ��� ������� Q
        if (Input.GetKeyDown(KeyCode.Q))
        {
            mainCamera.gameObject.SetActive(false);
            mapCamera.gameObject.SetActive(true);
            HpManaBar.gameObject.SetActive(false);
        }
        // ���������� �������� ������ ��� ���������� Q
        else if (Input.GetKeyUp(KeyCode.Q))
        {
            mapCamera.gameObject.SetActive(false);
            mainCamera.gameObject.SetActive(true);
            HpManaBar.gameObject.SetActive(true);
        }
    }
}