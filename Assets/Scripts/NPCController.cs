using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NPCController : MonoBehaviour
{
    public float interactionDistance = 2f;
    public GameObject dialogueBoxPrefab;
    public Transform playerTransform;
    public Canvas dialogueCanvas; 
    public string[] dialogueLines;
    private GameObject currentDialogueBox;
    private int currentLineIndex = 0;
    private bool canInteract = false;

    void Start()
    {
        if (playerTransform == null)
        {
            Debug.LogError("Не назначен Transform игрока! Перетащите Transform игрока в поле 'Player Transform' в инспекторе.");
            enabled = false;
        }
        if (dialogueCanvas == null)
        {
            Debug.LogError("Не назначен Canvas! Перетащите GameObject Canvas в поле 'Dialogue Canvas' в инспекторе.");
            enabled = false;
        }
    }

    void Update()
    {
        if (playerTransform != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);
            if (distanceToPlayer <= interactionDistance)
            {
                canInteract = true;
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (currentDialogueBox == null)
                    {
                        StartDialogue();
                    }
                    else
                    {
                        AdvanceDialogue();
                    }
                }
            }
            else
            {
                if (canInteract)
                {
                    canInteract = false;
                    EndDialogue();
                }
            }
        }
    }

    public void StartDialogue()
    {
        if (dialogueBoxPrefab != null && dialogueCanvas != null)
        {
            currentDialogueBox = Instantiate(dialogueBoxPrefab, Vector3.zero, Quaternion.identity);
            currentDialogueBox.transform.SetParent(dialogueCanvas.transform, false);
            currentDialogueBox.GetComponent<DialogueBox>().SetText(dialogueLines[0]);
            currentLineIndex = 0;
        }
        else
        {
            Debug.LogError("Не назначен префаб DialogueBox или Canvas! Проверьте инспектор NPC.");
        }
    }

    void AdvanceDialogue()
    {
        currentLineIndex++;
        if (currentLineIndex < dialogueLines.Length)
        {
            currentDialogueBox.GetComponent<DialogueBox>().SetText(dialogueLines[currentLineIndex]);
        }
        else
        {
            EndDialogue();
        }
    }

    public void EndDialogue()
    {
        if (currentDialogueBox != null)
        {
            Destroy(currentDialogueBox);
            currentDialogueBox = null;
            currentLineIndex = 0;
        }
    }
}