using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private GameObject dialogue; // UI panel for dialogue
    [SerializeField] private TextMeshProUGUI dialogueText; // Text component for dialogue

    private Queue<string> dialogueQueue = new Queue<string>(); // Stores dialogue lines
    private bool isDialogueActive = false;

    public void StartDialogue(string[] dialogueToPlay)
    {
        if (isDialogueActive) return;

        isDialogueActive = true;
        GameManager.Instance.playerMovement.LockState(true);
        dialogueQueue.Clear();

        foreach (string line in dialogueToPlay)
        {
            dialogueQueue.Enqueue(line);
        }

        dialogue.SetActive(true);
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
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
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        dialogue.SetActive(false);
        GameManager.Instance.playerMovement.LockState(false);
        isDialogueActive = false;
    }
}
