using UnityEngine;

public class AutoDestroyEffect : MonoBehaviour
{
    public float lifetime = 0.5f; // Время жизни эффекта в секундах

    void Start()
    {
        // Уничтожить этот игровой объект (к которому прикреплен скрипт)
        // через 'lifetime' секунд
        Destroy(gameObject, lifetime);
    }
}