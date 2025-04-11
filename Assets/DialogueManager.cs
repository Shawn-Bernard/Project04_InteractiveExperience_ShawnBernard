using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class DialogueManager : MonoBehaviour
{
    public GameObject dialogue;
    public TextMeshProUGUI dialogueText;

    private Queue<string> dialogueQueue = new Queue<string>();
    private string[] lastDialogue;
    private bool isDialogueActive
    {
        get
        {
            //returns true when active
            return dialogue.activeInHierarchy;
        }
    }

    public void StartDialogue(string[] dialogueToPlay)
    {
        // Only adding once, when starting a new dialogue
        if (!isDialogueActive)
        {
            PlayerInputActions.InteractEvent += AdvanceDialogue;
        }

        lastDialogue = dialogueToPlay;

        // If dialogue is already active and it's the same lines, don't reset
        if (isDialogueActive && lastDialogue == dialogueToPlay)
        {
            Debug.Log("returning cause dialogue is started");
            return;
        }

        dialogueQueue.Clear();

        // Adding each string in dialogue into the queue 
        foreach (string line in dialogueToPlay)
        {
            dialogueQueue.Enqueue(line);
        }

        DialogueBoolSet(true);

        AdvanceDialogue();
    }

    public void AdvanceDialogue()
    {
        if (dialogueQueue.Count == 0)
        {
            EndDialogue();
            return;
        }

        dialogueText.text = dialogueQueue.Dequeue();

    }

    private void EndDialogue()
    {
        PlayerInputActions.InteractEvent -= AdvanceDialogue;
        DialogueBoolSet(false);
    }

    private void DialogueBoolSet(bool value)
    {
        GameManager.Instance.playerMovement.LockState(value);
        dialogue.SetActive(value);

        if (value)
        {
            Cursor.lockState = CursorLockMode.Confined;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}