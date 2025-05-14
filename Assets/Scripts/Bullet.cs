using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] float speed;
    [SerializeField] float deathTime;

    void Start()
    {
        Invoke(nameof(Death), deathTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * Time.deltaTime * speed);
    }


    void Death()
    {
        Destroy(gameObject);
    }

}
