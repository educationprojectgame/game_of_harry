using UnityEngine;
using UnityEngine.UI;

public class TabImageController : MonoBehaviour
{
    public KeyCode toggleKey = KeyCode.Tab; // ������� ��� ����������
    public Image targetImage; // ������ �� ��������� Image
    public string imagePath = "Education"; // ���� � ����������� � Resources (��� ����������)

    void Start()
    {
        // ��������� ������ �� ��������
        Sprite newSprite = Resources.Load<Sprite>(imagePath);

        // ���������, ��� ������ ������� ��������
        if (newSprite != null && targetImage != null)
        {
            targetImage.sprite = newSprite; // ������������� ������ � ��������� Image
            if (PlayerPrefs.GetInt("ShowTab") == 1)
            {
                targetImage.enabled = true; // ��������� ����������� ��� ������
                PlayerPrefs.SetInt("ShowTab", 0);
            }

            else
                targetImage.enabled = false;
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