using UnityEngine;

public class BulletCollision : MonoBehaviour
{
    private void Start()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // �����������: ��������� ���, ����� �� ������������ �� "�����"
        if (!other.CompareTag("PlayerGame") && !other.CompareTag("WandCollider") && !other.CompareTag("Water"))
        {
            Destroy(gameObject);
        }
    }
}