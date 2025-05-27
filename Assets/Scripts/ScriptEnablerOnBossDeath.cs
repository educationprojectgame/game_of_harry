using UnityEngine;

public class ScriptEnablerOnBossDeath : MonoBehaviour
{
    [Tooltip("Ссылка на объект босса.")]
    public GameObject boss;

    [Tooltip("Скрипт EnterDoor, который нужно включить/выключить.")]
    [SerializeField] private EnterDoor enterDoorScript;


    void Start()
    {
        if (enterDoorScript == null)
        {
            Debug.LogError("EnterDoor Script To Toggle is not assigned.  Disabling this script.");
            enabled = false;
            return;
        }
        if (boss == null)
        {
            Debug.LogError("Boss object is not assigned.  Disabling this script.");
            enabled = false;
            return;
        }
    }

    void Update()
    {
        if (boss == null) // Проверяем, существует ли босс (был ли уничтожен)
        {
            // Босс побежден, включаем скрипт
            if (!enterDoorScript.enabled)
            {
                enterDoorScript.enabled = true;
                Debug.Log($"Script {enterDoorScript.GetType().Name} enabled because boss is defeated.");
            }
            enabled = false;
        }
        else
        {
            // Босс жив, отключаем скрипт
            if (enterDoorScript.enabled)
            {
                enterDoorScript.enabled = false;
                Debug.Log($"Script {enterDoorScript.GetType().Name} disabled because boss is alive.");
            }
        }
    }
}