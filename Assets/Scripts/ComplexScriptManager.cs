using UnityEngine;
using System.Collections.Generic; // Для использования List<>

public class ComplexScriptManager : MonoBehaviour
{
    public List<MonoBehaviour> scriptsToDisableAtStart = new List<MonoBehaviour>(); // Список скриптов для отключения
    public NPCController npcController; // Ссылка на NPCController
    public Camera cameraToEnable; // Камера для включения во время диалога
    public Camera cameraToDisable; // Камера для отключения во время диалога (также основная камера)
    public KeyCode activationKey = KeyCode.E; // Клавиша активации
    public int requiredPresses = 3; // Количество нажатий клавиши

    private int pressCount = 0;
    private bool scriptsDisabled = false; // Флаг, чтобы отслеживать, были ли скрипты отключены

    void Start()
    {
        // Проверки на null
        if (npcController == null)
        {
            Debug.LogError("NPCController not assigned!");
            enabled = false;
            return;
        }
         if (cameraToEnable == null)
        {
            Debug.LogError("cameraToEnable not assigned!");
            enabled = false;
            return;
        }
         if (cameraToDisable == null)
        {
            Debug.LogError("cameraToDisable not assigned!");
            enabled = false;
            return;
        }

        // Отключаем указанные скрипты
        foreach (MonoBehaviour script in scriptsToDisableAtStart)
        {
            if (script != null)
            {
                script.enabled = false;
            }
            else
            {
                Debug.LogWarning("Null script in scriptsToDisableAtStart list!");
            }
        }

        // Запускаем диалог у NPC
        npcController.StartDialogue();
        cameraToDisable.enabled = false; // Отключаем основную камеру
        cameraToEnable.enabled = true;   // Включаем камеру для диалога

        scriptsDisabled = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(activationKey))
        {
            pressCount++;

            if (pressCount >= requiredPresses)
            {
                // Включаем обратно отключенные скрипты
                foreach (MonoBehaviour script in scriptsToDisableAtStart)
                {
                    if (script != null)
                    {
                        script.enabled = true;
                    }
                }

                // Останавливаем диалог у NPC
                npcController.EndDialogue(); // Используем EndDialogue()
                cameraToEnable.enabled = false;   // Отключаем камеру для диалога
                cameraToDisable.enabled = true;    // Включаем обратно основную камеру

                // Отключаем этот скрипт, чтобы больше не проверять нажатия (опционально)
                enabled = false;
                Debug.Log("Scripts re-enabled, NPC stopped, cameras switched!");
            }
        }
    }
}