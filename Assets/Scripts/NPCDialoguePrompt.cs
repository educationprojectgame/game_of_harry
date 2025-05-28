using UnityEngine;
using UnityEngine.UI;

public class NPCDialoguePrompt : MonoBehaviour
{
    [Tooltip("Ссылка на объект игрока.")]
    public GameObject player;

    [Tooltip("Ссылка на объект NPC.")]
    public GameObject npc;

    [Tooltip("Ссылка на Canvas, на котором отображается текст.")]
    public Canvas dialogueCanvas;

    [Tooltip("Ссылка на UI Text элемент для отображения подсказки.")]
    public Text promptTextObject;

    [Tooltip("Расстояние, на котором появляется текст 'Говорить - E'.")]
    public float interactionDistance = 3.0f;

    [Tooltip("Фиксированная высота текста над NPC (в мировых координатах).")]
    public float textWorldHeightOffset = 2.0f;

    private Camera mainCamera; // Ссылка на основную камеру

    private bool hasTextComponent = true;

    void Start()
    {
        // Проверяем, что все необходимые ссылки назначены
        if (player == null)
        {
            Debug.LogError("Player object is not assigned. Disabling script.");
            enabled = false;
            return;
        }
        if (npc == null)
        {
            Debug.LogError("NPC object is not assigned. Disabling script.");
            enabled = false;
            return;
        }
        if (dialogueCanvas == null)
        {
            Debug.LogError("Dialogue Canvas is not assigned. Disabling script.");
            enabled = false;
            return;
        }
        if (promptTextObject == null)
        {
            Debug.LogError("Prompt Text Object is not assigned. Disabling script.");
            enabled = false;
            return;
        }
        else
        {
            // Проверяем есть ли Text Component
            Text textComponent = promptTextObject.GetComponent<Text>();
            if (textComponent == null)
            {
                Debug.LogError("Prompt Text Object has no Text Component. Disabling script.");
                enabled = false;
                hasTextComponent = false;
                return;
            }
        }

        // Получаем ссылку на основную камеру
        mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("Main Camera not found.  Make sure a camera in the scene is tagged as MainCamera.");
            enabled = false;
            return;
        }
    }

    void Update()
    {
        // Вычисляем расстояние между игроком и NPC
        if (hasTextComponent)
        {
            float distance = Vector3.Distance(player.transform.position, npc.transform.position);

            // Если расстояние меньше заданного, показываем текст и обновляем позицию, иначе скрываем
            if (distance <= interactionDistance)
            {
                promptTextObject.gameObject.SetActive(true);
                UpdateTextPosition();
            }
            else
            {
                promptTextObject.gameObject.SetActive(false);
            }
        }
    }

    void UpdateTextPosition()
    {
        // Вычисляем позицию текста в мировом пространстве
        Vector3 npcWorldPosition = npc.transform.position;
        Vector3 textWorldPosition = npcWorldPosition + new Vector3(0, textWorldHeightOffset, 0); // Add the specified height offset
        Vector2 viewportPosition = mainCamera.WorldToViewportPoint(textWorldPosition);

        // Преобразуем позицию в мировом пространстве в позицию на Canvas
        RectTransform canvasRectTransform = dialogueCanvas.GetComponent<RectTransform>();
        Vector2 anchoredPosition;

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, new Vector2(viewportPosition.x * Screen.width, viewportPosition.y * Screen.height), mainCamera, out anchoredPosition))
        {
            // Устанавливаем позицию UI Text
            RectTransform textRectTransform = promptTextObject.GetComponent<RectTransform>();
            textRectTransform.anchoredPosition = anchoredPosition;
        }
    }
}