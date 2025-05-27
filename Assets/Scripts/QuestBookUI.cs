using UnityEngine;

public class QuestBookUI : MonoBehaviour
{
    [SerializeField] private Animator questBookAnimator;
    [SerializeField] private GameObject questBookPanel;

    private bool isOpen = false;

    void Start()
    {
        questBookPanel.SetActive(false); // Скрыть при старте
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            ToggleQuestBook();
        }
    }

    public void ToggleQuestBook()
    {
        if (isOpen)
        {
            questBookAnimator.SetTrigger("Close");
            isOpen = false;
            // можно скрыть чуть позже, в конце анимации
            Invoke("HidePanel", 0.5f); // время в секундах
        }
        else
        {
            questBookPanel.SetActive(true);
            questBookAnimator.SetTrigger("Open");
            isOpen = true;
        }
    }

    private void HidePanel()
    {
        if (!isOpen)
            questBookPanel.SetActive(false);
    }
}
