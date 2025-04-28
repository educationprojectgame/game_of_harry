using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DialogueBox : MonoBehaviour
{
    public RectTransform dialoguePanel;
    public Text dialogueText;
    public float animationDuration = 0.5f;
    public float slideInPosition = 0f;
    public float slideOutPosition = -200f;
    private Coroutine animationCoroutine;

    void Start()
    {
        if (dialoguePanel != null) dialoguePanel.anchoredPosition = new Vector2(dialoguePanel.anchoredPosition.x, slideOutPosition);
        else Debug.LogError("Dialogue Panel is not assigned! Please assign it in the Inspector.");
    }

    public void ShowDialogue(string text)
    {
        if (animationCoroutine != null) StopCoroutine(animationCoroutine);
        if (dialogueText != null) dialogueText.text = text;
        else
        {
            Debug.LogError("Dialogue Text is not assigned! Please assign it in the Inspector.");
            return;
        }
        animationCoroutine = StartCoroutine(SlideIn());
    }

    public void HideDialogue()
    {
        if (animationCoroutine != null) StopCoroutine(animationCoroutine);
        animationCoroutine = StartCoroutine(SlideOut());
    }

    private IEnumerator SlideIn()
    {
        if (dialoguePanel == null)
        {
            Debug.LogError("Dialogue Panel is not assigned!");
            yield break;
        }
        float timeElapsed = 0;
        Vector2 startPosition = dialoguePanel.anchoredPosition;
        Vector2 targetPosition = new Vector2(dialoguePanel.anchoredPosition.x, slideInPosition);
        while (timeElapsed < animationDuration)
        {
            dialoguePanel.anchoredPosition = Vector2.Lerp(startPosition, targetPosition, timeElapsed / animationDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        dialoguePanel.anchoredPosition = targetPosition;
        animationCoroutine = null;
    }

    private IEnumerator SlideOut()
    {
        if (dialoguePanel == null)
        {
            Debug.LogError("Dialogue Panel is not assigned!");
            yield break;
        }
        float timeElapsed = 0;
        Vector2 startPosition = dialoguePanel.anchoredPosition;
        Vector2 targetPosition = new Vector2(dialoguePanel.anchoredPosition.x, slideOutPosition);
        while (timeElapsed < animationDuration)
        {
            dialoguePanel.anchoredPosition = Vector2.Lerp(startPosition, targetPosition, timeElapsed / animationDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        dialoguePanel.anchoredPosition = targetPosition;
        animationCoroutine = null;
    }
}
