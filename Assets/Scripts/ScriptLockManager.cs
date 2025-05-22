using UnityEngine;

public class ScriptLockManager : MonoBehaviour
{
    public MonoBehaviour script1;
    public MonoBehaviour script2;
    public MonoBehaviour script3;
    public KeyCode activationKey = KeyCode.E;
    public int requiredPresses = 3;
    private int pressCount = 0;

    void Start()
    {
        if (script1 == null || script2 == null || script3 == null)
        {
            Debug.LogError("One or more scripts are not assigned in the Inspector!");
            enabled = false;
            return;
        }
        script1.enabled = false;
        script2.enabled = false;
        script3.enabled = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(activationKey))
        {
            pressCount++;
            if (pressCount >= requiredPresses)
            {
                script1.enabled = true;
                script2.enabled = true;
                script3.enabled = false;
                enabled = false;
                Debug.Log("Scripts unlocked!");
            }
             else
            {
                Debug.Log("Presses left: " + (requiredPresses - pressCount));
            }
        }
    }
}