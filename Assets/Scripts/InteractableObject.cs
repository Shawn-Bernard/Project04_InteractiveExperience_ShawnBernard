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

    [SerializeField] private string QuestName;

    public Interaction InteractionType;

    [SerializeField] private GameObject ItemTrigger;

    [SerializeField] private GameObject Givenitem;

    [SerializeField] private string[] InfoString;

    [SerializeField] private string[] Dialogue;// dialogue to play when first start to npc

    [SerializeField] private string[] questDialogue;//to play when quest has started

    [SerializeField] private string[] hasItemDialogue;//plays when the item is given

    [SerializeField] private bool talkedTo;

    public bool hasItem
    {
        get
        {
            if (ItemTrigger != null)
            {
                if (!gameManager.inventoryManager)
                {
                    gameManager = FindObjectOfType<GameManager>();
                }
                if (gameManager.inventoryManager.HasItemInInventory(ItemTrigger))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }
    }

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();

        if (gameManager == null)
        {
            Debug.LogError("GameManager not found in scene!");
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

        // Store the prefab, not the scene object
        GameManager.Instance.inventoryManager.AddToInventory(new GameObject(gameObject.name));
    }

    public void Info()
    {
        if (gameManager.questManager.IsQuestDone(QuestName))
        {
            Debug.Log("Quest is done");
            if (Givenitem != null)
            {
                Debug.Log("item is added");
                GameManager.Instance.inventoryManager.AddToInventory(Givenitem);
            }

        }
        else if (gameManager.questManager.HasQuestActive(QuestName))
        {
            if (Givenitem == null)
            {
                Debug.Log("item is added");
                PickUp();
            }
        }
        else
        {
            gameManager.UImanager.StartCoroutine(gameManager.UImanager.StartInfo(InfoString));
        }
    }

    public void StartDialogue()
    {
        Debug.Log("Dialogue triggered");

        string[] dialogueToPlay;

        if (hasItem)
        {
            Debug.Log("Player has item");
            GameManager.Instance.questManager.CompleteQuest(QuestName);
            dialogueToPlay = hasItemDialogue;  // Dialogue for when the player has the item
            gameManager.dialogueManager.StartDialogue(dialogueToPlay);
            if (Givenitem)
            {
                GameManager.Instance.inventoryManager.AddToInventory(Givenitem);
            }
        }
        else if (talkedTo && !hasItem)
        {
            Debug.Log("Quest dialogue asking for item");
            dialogueToPlay = questDialogue;  // Dialogue for when the player asking the player about the item
            gameManager.dialogueManager.StartDialogue(dialogueToPlay);
        }
        else
        {
            Debug.Log("First time talking playing dialogue");
            dialogueToPlay = Dialogue;  // Dialogue for first time conversation
            gameManager.dialogueManager.StartDialogue(dialogueToPlay);
            talkedTo = true;  // Mark that the player has talked to the NPC
            return;
        }
        GameManager.Instance.questManager.AddNewQuest(QuestName);
    }

}
