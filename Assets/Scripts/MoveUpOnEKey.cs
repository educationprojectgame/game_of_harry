using UnityEngine;

public class MoveUpOnEKey : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float activationThreshold = 10f;
    public float moveDistance = 5f;
    public Sprite newSprite;
    private int eKeyPressCount = 0;
    private bool isMovingUp = false;
    private float distanceMoved = 0f;
    private SpriteRenderer spriteRenderer;
    private Sprite originalSprite; 

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer component not found on this GameObject!");
            enabled = false;
            return;
        }
        originalSprite = spriteRenderer.sprite;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            eKeyPressCount++;
            if (eKeyPressCount == activationThreshold)
            {
                StartMovingUp();
            }
        }
        if (isMovingUp)
        {
            float step = moveSpeed * Time.deltaTime;

            if (distanceMoved + step > moveDistance)
            {
                step = moveDistance - distanceMoved;
            }
            transform.Translate(Vector2.up * step);
            distanceMoved += step;
            if (distanceMoved >= moveDistance)
            {
                StopMovingUp();
            }
        }
    }

    void StartMovingUp()
    {
        isMovingUp = true;
        distanceMoved = 0f;
        if (newSprite != null && spriteRenderer != null)
        {
            spriteRenderer.sprite = newSprite;
        }
        else
        {
            Debug.LogWarning("New sprite not assigned or SpriteRenderer is missing!");
        }
        Debug.Log("Начато движение вверх!");
    }

    void StopMovingUp()
    {
         isMovingUp = false;
        if (spriteRenderer != null)
        {
            spriteRenderer.sprite = originalSprite;
        }
        Debug.Log("Движение вверх остановлено!");
    }
}