using UnityEngine;

public class Shield : MonoBehaviour
{
    public float duration = 3f;

    void Start()
    {
        Destroy(gameObject, duration);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("EnemyProjectile"))
        {
            Destroy(other.gameObject); // ўит поглощает снар€д
        }
    }
}