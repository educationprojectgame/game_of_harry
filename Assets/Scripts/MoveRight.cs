using UnityEngine;

public class MoveRight : MonoBehaviour
{
    public float speed = 2f;
    public float distance = 5f;
    public NPCController npcController;

    private Vector3 startPosition;
    private Vector3 targetPosition;
    private bool isMoving = false;

    void Start()
    {
        startPosition = transform.position;
        targetPosition = startPosition + Vector3.right * distance;
        isMoving = true;
    }

    void Update()
    {
        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

            if (transform.position == targetPosition)
            {
                isMoving = false;
                if (npcController != null)
                {
                    npcController.StartDialogue();
                }
            }
        }
    }
}