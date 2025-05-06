using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FloatingPrompt : MonoBehaviour
{
    public Transform npcTransform;
    public Text promptText;
    public float offsetY = 1.5f;
    public float showDistance = 5f;
    public Transform playerTransform;

    void Start()
    {
        if (npcTransform == null)
        {
            Debug.LogError("NPC Transform not assigned!", this);
            enabled = false;
            return;
        }

        if (promptText == null)
        {
            Debug.LogError("Prompt Text not assigned!", this);
            enabled = false;
            return;
        }

        if (playerTransform == null)
        {
            Debug.LogError("Player Transform not assigned!", this);
            enabled = false;
            return;
        }

        promptText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (npcTransform == null || promptText == null || playerTransform == null)
        {
            return;
        }
        float distanceToPlayer = Vector3.Distance(npcTransform.position, playerTransform.position);
        if (distanceToPlayer <= showDistance)
        {
            ShowPrompt();
        }
        else
        {
            HidePrompt();
        }
        if (promptText.gameObject.activeSelf)
        {
            promptText.transform.position = npcTransform.position + Vector3.up * offsetY;
        }
    }

    void ShowPrompt()
    {
        if (promptText != null)
        {
            promptText.gameObject.SetActive(true);
        }
    }

    void HidePrompt()
    {
        if (promptText != null)
        {
            promptText.gameObject.SetActive(false);
        }
    }
}