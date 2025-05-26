using UnityEngine;
using System.Collections.Generic;

public class DialogueManager : MonoBehaviour
{
    [Tooltip("List of scripts to disable during dialogue.")]
    public List<MonoBehaviour> scriptsToDisable = new List<MonoBehaviour>();

    [Tooltip("The camera to disable during dialogue (usually the main camera).")]
    public Camera cameraToDisable;

    [Tooltip("The camera to enable during dialogue.")]
    public Camera cameraToEnable;

    [Tooltip("The NPCController script responsible for the dialogue.")]
    public NPCController npcController;

    [Tooltip("The key to press to advance the dialogue.")]
    public KeyCode activationKey = KeyCode.E;

    [Tooltip("Number of key presses needed to exit the dialogue.")]
    public int requiredPresses = 3;

    private int pressCount = 0;
    private bool inDialogue = false; // Tracks if we're in the dialogue state.
    private List<bool> scriptStates = new List<bool>(); //Store original states
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

        // Store original states of the scripts so they can be restored later.
        foreach (var script in scriptsToDisable)
        {
            if (script != null)
            {
                scriptStates.Add(script.enabled);
            }
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
        cameraToDisable.enabled = false;
        cameraToEnable.enabled = true;
        camerasSwitched = true; // Make Sure that we track if cameras were Switched


        //Start NPC dialogue
        npcController.StartDialogue(); //Assumes StartDialogue exists on the NPC Controller

        Debug.Log("Dialogue Started: Scripts disabled, cameras switched, NPC dialogue started.");
    }



    //Method to end the dialogue
    private void EndDialogue()
    {
        inDialogue = false;  //we are no longer in Dialogue

        //Re-enable Scripts to original state.
        for (int i = 0; i < scriptsToDisable.Count; i++)
        {
            if (scriptsToDisable[i] != null)
            {
                scriptsToDisable[i].enabled = scriptStates[i]; //Restore Original State
            }
        }

        //Re-enable / disable cameras
        cameraToEnable.enabled = false;
        cameraToDisable.enabled = true;
        camerasSwitched = false;

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
            cameraToEnable.enabled = false;
            cameraToDisable.enabled = true;
        }
    }
}