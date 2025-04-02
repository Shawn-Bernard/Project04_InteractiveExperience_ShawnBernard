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

    public GameObject ItemTrigger;

    public string[] InfoString;

    public string[] Dialogue;

    public string[] ItemDialogue;

    [SerializeField]
    public bool hasItem
    {
        get
        {
            if (GameManager.Instance.playerInteraction.Inventory.Contains(ItemTrigger))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
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
        Debug.Log("Dialogue");
        if (hasItem)
        {
            Debug.Log("Player has item");
            Dialogue = ItemDialogue;
            GameManager.Instance.dialogueManager.StartDialogue(Dialogue);
        }
        else
        {
            GameManager.Instance.dialogueManager.StartDialogue(Dialogue);
        }
        Debug.Log("if has ended");
        



    }
}
