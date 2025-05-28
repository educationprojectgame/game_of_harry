using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeDialogue : MonoBehaviour
{
    private bool playerInRange = false;
    public float interactionDistance = 2f;
    public GameObject dialogueBoxPrefab;
    public Transform playerTransform;
    public Canvas dialogueCanvas;
    public string[] dialogueLines;
    public string[] newDialogueLines;
    private GameObject currentDialogueBox;
    private int currentLineIndex = 0;
    private bool canInteract = false;

    void Start()
    {
        if (playerTransform == null)
        {
            Debug.LogError("�� �������� Transform ������! ���������� Transform ������ � ���� 'Player Transform' � ����������.");
            enabled = false;
        }
        if (dialogueCanvas == null)
        {
            Debug.LogError("�� �������� Canvas! ���������� GameObject Canvas � ���� 'Dialogue Canvas' � ����������.");
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
                        if (PlayerPrefs.GetInt("CrystallIsTaken") == 1)
                            AdvanceDialogue(newDialogueLines.Length, true);
                        else
                            AdvanceDialogue(dialogueLines.Length, false);
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
            if (PlayerPrefs.GetInt("CrystallIsTaken") == 1)
                currentDialogueBox.GetComponent<DialogueBox>().SetText(newDialogueLines[0]);
            else
                currentDialogueBox.GetComponent<DialogueBox>().SetText(dialogueLines[0]);
            currentLineIndex = 0;
        }
        else
        {
            Debug.LogError("�� �������� ������ DialogueBox ��� Canvas! ��������� ��������� NPC.");
        }
    }

    void AdvanceDialogue(int length, bool isNewDialogue)
    {
        currentLineIndex++;
        if (currentLineIndex < length)
        {
            var dialogue = (isNewDialogue) ? newDialogueLines : dialogueLines;
            currentDialogueBox.GetComponent<DialogueBox>().SetText(dialogue[currentLineIndex]);
        }
        else
        {
            if (isNewDialogue)
            {
                EndDialogue();
                SceneManager.LoadScene("Menu");
            }
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
