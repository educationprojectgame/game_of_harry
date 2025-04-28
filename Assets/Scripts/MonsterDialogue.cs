using UnityEngine;

public class MonsterDialogue : MonoBehaviour
{
    public DialogueBox dialogueBox;
    public KeyCode showDialogueKey = KeyCode.Space;
    public string dialogueText = "������, ���!";
    public float activationDistance = 3f;
    public Transform player;
    
    private bool dialogueIsVisible = false;

    void Start()
    {
        if (player == null)
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            if (playerObject != null) player = playerObject.transform;
            else
            {
                Debug.LogWarning("Player not assigned and no GameObject with tag 'Player' found!  " +
                    "Please assign the Player Transform in the Inspector or tag your Player GameObject with 'Player'.");
            }
        }
    }

    void Update()
    {
        if (player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);
            if (distanceToPlayer <= activationDistance)
            {
                if (Input.GetKeyDown(showDialogueKey))
                {
                    if (dialogueBox != null)
                    {
                        if (dialogueIsVisible)
                        {
                            dialogueBox.HideDialogue();
                            dialogueIsVisible = false;
                        }
                        else
                        {
                            dialogueBox.ShowDialogue(dialogueText);
                            dialogueIsVisible = true;
                        }
                    }
                    else Debug.LogError("DialogueBox is not assigned! Please assign it in the Inspector.");
                }
            }
            else
            {
                if (dialogueIsVisible)
                {
                    dialogueBox.HideDialogue();
                    dialogueIsVisible = false;
                }
            }
        }
        else
        {
            Debug.LogError("Player Transform is not assigned! " +
                "Please assign it in the Inspector or tag your Player GameObject with 'Player'.");
        }
    }
}
