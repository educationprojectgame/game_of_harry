using UnityEngine;
using System.Collections.Generic;

public class DialogueManager : MonoBehaviour
{
    public List<MonoBehaviour> scriptsToDisable = new List<MonoBehaviour>();
    public Camera cameraToDisable;
    public Camera cameraToEnable;
    public NPCController npcController;
    public KeyCode activationKey = KeyCode.E;
    public int requiredPresses = 3;
    private int pressCount = 0;
    private bool inDialogue = false; // Tracks if we're in the dialogue state.
     private bool camerasSwitched = false;

    void Start()
    {
        // Error Checking to ensure proper setup in the editor
        if (scriptsToDisable == null || scriptsToDisable.Count == 0)
        {
            Debug.LogWarning("No scripts assigned to disable.  Dialogue will still run, but no scripts will be disabled.");
        }

        if (cameraToDisable == null || cameraToEnable == null)
        {
            Debug.LogError("Both cameraToDisable and cameraToEnable must be assigned. Disabling script");
            enabled = false;
            return;
        }

        if (npcController == null)
        {
            Debug.LogError("NPCController must be assigned.  Disabling script");
            enabled = false;
            return;
        }

        StartDialogue();  // Start the Dialogue Immediately
    }

    void Update()
    {
        //Dialogue activation logic
        if (inDialogue)
        {
            if (Input.GetKeyDown(activationKey))
            {
                pressCount++;

                if (pressCount >= requiredPresses)
                {
                    EndDialogue();
                }
            }
        }
    }


    //Method to start the dialogue
    private void StartDialogue()
    {
        inDialogue = true;  //We are now in Dialogue.

        //Disable Scripts
        for (int i = 0; i < scriptsToDisable.Count; i++)
        {
            if (scriptsToDisable[i] != null)
            {
                scriptsToDisable[i].enabled = false;
            }
        }


        //Disable/Enable cameras
        cameraToDisable.gameObject.SetActive(false);
        cameraToEnable.gameObject.SetActive(true);
        camerasSwitched = true; // Make Sure that we track if cameras were Switched


        //Start NPC dialogue
        npcController.StartDialogue(); //Assumes StartDialogue exists on the NPC Controller

        Debug.Log("Dialogue Started: Scripts disabled, cameras switched, NPC dialogue started.");
    }



    //Method to end the dialogue
    private void EndDialogue()
    {
        inDialogue = false;  //we are no longer in Dialogue
        cameraToEnable.gameObject.SetActive(false);
        cameraToDisable.gameObject.SetActive(true);
        camerasSwitched = false;
        //Re-enable Scripts to original state.
        for (int i = 0; i < scriptsToDisable.Count; i++)
        {
            if (scriptsToDisable[i] != null)
            {
                scriptsToDisable[i].enabled = true; //Restore Original State
            }
        }

        //End NPC dialogue
        npcController.EndDialogue(); //Assumes EndDialogue exists on the NPC Controller

        Debug.Log("Dialogue Ended: Scripts re-enabled, cameras switched back, NPC dialogue ended.  Disabling Script Manager");
        enabled = false;  //Disable this script from running.
    }

    //Clean Up when Script is Disabled.
    private void OnDisable()
    {
        //If script is disabled and dialogue is still in progress ensure we shut it down.
        if (inDialogue)
        {
            EndDialogue();
        }
         if(camerasSwitched){
            cameraToEnable.gameObject.SetActive(false);
            cameraToDisable.gameObject.SetActive(true);
        }
    }
}