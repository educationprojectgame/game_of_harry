using UnityEngine;

public class CloudMover : MonoBehaviour
{
    public float speed; // скорость движения
    public float resetPositionX; // куда переместить облако, когда оно уходит за экран
    public float startPositionX; // начальная позиция, с которой облако появляется

    void Update()
    {
        // Движение облака влево
        transform.position += Vector3.left * speed * Time.deltaTime;

        // Если облако ушло за экран слева — возвращаем его вправо
        if (transform.position.x < resetPositionX)
        {
            transform.position = new Vector3(startPositionX, transform.position.y, transform.position.z);
        }
    }
}