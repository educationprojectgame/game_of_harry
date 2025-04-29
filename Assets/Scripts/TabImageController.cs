using UnityEngine;
using UnityEngine.UI;

public class TabImageController : MonoBehaviour
{
    public KeyCode toggleKey = KeyCode.Tab; // ������� ��� ����������
    public Image targetImage; // ������ �� ��������� Image
    public string imagePath = "testt"; // ���� � ����������� � Resources (��� ����������)

    void Start()
    {
        // ��������� ������ �� ��������
        Sprite newSprite = Resources.Load<Sprite>(imagePath);

        // ���������, ��� ������ ������� ��������
        if (newSprite != null && targetImage != null)
        {
            targetImage.sprite = newSprite; // ������������� ������ � ��������� Image
            targetImage.enabled = true; // ��������� ����������� ��� ������
        }
        else
        {
            Debug.LogError("�� ������� ��������� ������ ��� ��������� Image �� �����.");
        }
    }

    void Update()
    {
        if (targetImage == null) return;

        // �������� ����������� ��� �������
        if (Input.GetKeyDown(toggleKey))
        {
            targetImage.enabled = true;
        }

        // ��������� ��� ����������
        if (Input.GetKeyUp(toggleKey))
        {
            targetImage.enabled = false;
        }
    }
}