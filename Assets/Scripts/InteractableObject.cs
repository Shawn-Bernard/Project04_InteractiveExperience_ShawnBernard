using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class InteractableObject : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
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

    public string[] Dialogue;// dialogue to play when first start to npc

    public string[] questDialogue;//to play when quest has started

    public string[] ItemDialogue;//plays when the item is given


    public void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    [SerializeField]
    private bool hasItem
    {
        get
        {
            if (GameManager.Instance.playerInteraction.Inventory.Contains(ItemTrigger))
            {
                Dialogue = ItemDialogue;
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
        gameManager.UImanager.StartCoroutine(gameManager.UImanager.StartInfo(InfoString));
    }

    public void StartDialogue()
    {
        Debug.Log("Dialogue");
        string[] DialogueToPlay = Dialogue;
        for (int i = 0; i < 1; i++)
        {
            gameManager.dialogueManager.StartDialogue(DialogueToPlay);
        }

        if (hasItem)
        {
            Debug.Log("Player has item");
            gameManager.dialogueManager.StartDialogue(Dialogue);
        }
        else if (!hasItem && Dialogue != questDialogue)
        {
            Debug.Log("!Player does not have item");
            Dialogue = questDialogue;
            gameManager.dialogueManager.StartDialogue(Dialogue);
        }
    }
}
