using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueBox : MonoBehaviour
{
    public Text dialogueText;

    public void SetText(string text)
    {
        dialogueText.text = text;
    }
}