using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private GameObject dialogue; // UI panel for dialogue
    [SerializeField] private TextMeshProUGUI dialogueText; // Text component for dialogue

    [SerializeField] private Queue<string> dialogueQueue = new Queue<string>(); // Stores dialogue lines
    [SerializeField]
    private bool isDialogueActive
    {
        get
        {
            if (dialogue.activeInHierarchy == true)
            {
                Debug.Log("dialogue is active");
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
                return true;
            }
            else
            {
                Debug.Log("dialogue is not active");
                return false;
            }
        }
    }

    public void StartDialogue(string[] dialogueToPlay)
    {
        Debug.Log("Start dialogue");
        
        if (isDialogueActive)
        {
            Debug.Log("returning cause dialogue is started");
            return;
        }

        GameManager.Instance.playerMovement.LockState(true);
        dialogueQueue.Clear();
        foreach (string line in dialogueToPlay)
        {
            dialogueQueue.Enqueue(line);
        }

        dialogue.SetActive(true);

        dialogueText.text = dialogueQueue.Dequeue();
    }

    public void AdvanceDialogue()
    {
        if (dialogueQueue.Count == 0)
        {
            Debug.Log("Ending dialogue cause queue is done");
            EndDialogue();
        }

        dialogueText.text = dialogueQueue.Dequeue();
    }

    private void EndDialogue()
    {
        Debug.Log("End dialogue method");
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        GameManager.Instance.playerMovement.LockState(false);
        dialogue.SetActive(false);
        
    }
}
