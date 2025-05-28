using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class TextSequenceVerticalToggleInstant : MonoBehaviour
{
    public Text textDisplay;
    public string[] lines;
    public float delayBeforeStart = 1.0f;
    public float delayBeforeEnd = 1.0f;
    public float verticalSpacing = 20.0f;
    public Vector2 startPosition = new Vector2(0, 0);
    public Canvas parentCanvas;
    public GameObject panelToCheck;
    private bool sequenceRunning = false;
    private bool textVisible = false;
    private List<GameObject> textObjects = new List<GameObject>();

    void Start()
    {
        if (parentCanvas == null)
        {
            Debug.LogError("No Canvas found in parents of textDisplay. Disabling script.");
            enabled = false;
            return;
        }
        if(panelToCheck == null)
        {
            Debug.LogWarning("Panel To Check is not assigned, the text will display as long as 'C' is pressed.");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) && !sequenceRunning)
        {
            StartCoroutine(ToggleTextSequence());
        }
    }

    IEnumerator ToggleTextSequence()
    {
        sequenceRunning = true;
        if (!textVisible)
        {
            yield return new WaitForSeconds(delayBeforeStart);
            if (panelToCheck == null || panelToCheck.activeInHierarchy)
            {
                ShowText();
                textVisible = true;
            }
            else
            {
                Debug.Log("Panel is disabled or does not exists");
                HideText();
                textVisible = false;
            }
        }
        else
        {
            yield return new WaitForSeconds(delayBeforeEnd);
            HideText();
            textVisible = false;
        }
        sequenceRunning = false;
    }

    void ShowText()
    {
        for (int i = 0; i < lines.Length; i++)
        {
            GameObject newTextObject = new GameObject("TextLine_" + i);
            newTextObject.transform.SetParent(textDisplay.transform.parent, false);

            Text newText = newTextObject.AddComponent<Text>();
            newText.font = textDisplay.font;
            newText.fontSize = textDisplay.fontSize;
            newText.color = textDisplay.color;
            newText.alignment = textDisplay.alignment;
            newText.text = lines[i];
            RectTransform rectTransform = newTextObject.GetComponent<RectTransform>();
            newText.horizontalOverflow = HorizontalWrapMode.Overflow;
            rectTransform.sizeDelta = new Vector2(500, rectTransform.sizeDelta.y);
            rectTransform.anchoredPosition = startPosition - new Vector2(0, i * verticalSpacing);
            textObjects.Add(newTextObject);
        }
    }

    void HideText()
    {
        foreach (GameObject textObject in textObjects)
        {
            Destroy(textObject);
        }
        textObjects.Clear();
    }
}