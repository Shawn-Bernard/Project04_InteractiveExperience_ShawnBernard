using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class InteractableObject : MonoBehaviour
{
    public enum Interaction
    {
        Nothing,
        Pickup,
        Info,
        Dialogue
    }

    public Interaction InteractionType;

    public string[] InfoString;
    public string[] Dialogue;

    public void Interact()
    {
        switch (InteractionType)
        {
            case Interaction.Nothing:
                Nothing();
                break;
            case Interaction.Pickup:
                PickUp();
                break;
            case Interaction.Info:
                Info();
                break;
            case Interaction.Dialogue:
                StartDialogue();
                break;
        }
    }

    public void Nothing()
    {
        Debug.Log("Nothing is here");
    }

    public void PickUp()
    {
        Debug.Log("Pickup");
        gameObject.SetActive(false);
    }

    public void Info()
    {
        GameManager.Instance.UImanager.StartCoroutine(GameManager.Instance.UImanager.StartInfo(InfoString));
    }

    public void StartDialogue()
    {
        GameManager.Instance.dialogueManager.StartDialogue(Dialogue);
    }
}
