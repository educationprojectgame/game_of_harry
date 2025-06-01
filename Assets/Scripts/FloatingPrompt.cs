using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FloatingPrompt : MonoBehaviour
{
    [Tooltip("Трансформ NPC, над которым отображается текст.")]
    public Transform npcTransform;

    [Tooltip("Текст подсказки (UI Text).")]
    public Text promptText;

    [Tooltip("Смещение текста по вертикали относительно NPC.")]
    public float offsetY = 1.5f;

    [Tooltip("Расстояние, на котором появляется подсказка (используется квадрат расстояния для оптимизации).")]
    public float showDistanceSquared = 25f; // 5 * 5 (квадрат 5)

    [Tooltip("Трансформ игрока.")]
    public Transform playerTransform;

    private void Awake()
    {
        //Ищем компоненты, если они не были назначены в инспекторе
        if (npcTransform == null) npcTransform = transform;

        if (playerTransform == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null) playerTransform = player.transform;
        }

        if (promptText == null)
        {
            Debug.LogError("Prompt Text not assigned!", this);
            enabled = false;
            return;
        }
        if (npcTransform == null || promptText == null || playerTransform == null)
        {
            Debug.LogError("One or more transforms not assigned", this);
            enabled = false;
            return;
        }
    }


    void Start()
    {
        promptText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (npcTransform == null || promptText == null || playerTransform == null)
        {
            return; // Защита от ошибок, если объекты были уничтожены
        }

        // Оптимизированное вычисление расстояния
        float distanceToPlayerSquared = (npcTransform.position - playerTransform.position).sqrMagnitude;

        if (distanceToPlayerSquared <= showDistanceSquared)
        {
            ShowPrompt();
        }
        else
        {
            HidePrompt();
        }

        // Обновление позиции текста, только если он активен
        if (promptText.gameObject.activeSelf)
        {
            promptText.transform.position = npcTransform.position + Vector3.up * offsetY;
        }
    }

    void ShowPrompt()
    {
        promptText.gameObject.SetActive(true);
    }

    void HidePrompt()
    {
        promptText.gameObject.SetActive(false);
    }

    // Отключаем скрипт, когда он больше не нужен
    private void OnDisable()
    {
        if (promptText != null)
        {
            promptText.gameObject.SetActive(false); // Скрываем текст при отключении
        }
    }

}