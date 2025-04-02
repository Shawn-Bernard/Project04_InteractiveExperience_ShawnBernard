using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] public List<GameObject> Inventory = new List<GameObject>();
    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameObject Current = null;
    GameObject Item;

    [SerializeField] private InteractableObject interactableObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {   
        Debug.Log("Here");
        if (other.gameObject.CompareTag("Interactable"))
        {
            Current = other.gameObject;
            PlayerInputActions.InteractEvent += Interact;
        }
        else
        {
            PlayerInputActions.InteractEvent -= Interact;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Interactable"))
        {
            PlayerInputActions.InteractEvent -= Interact;
            Current = null;
        }
    }

    public void Interact()
    {
        interactableObject = Current.GetComponent<InteractableObject>();
        //Checks if the interactableObject is a pickup or not so we can add it to our inventory 
        if (interactableObject.InteractionType == InteractableObject.Interaction.Pickup)
        {
            Item = Current;
            Inventory.Add(Item);

            string inventoryText = "";

            for (int i = 0; i < Inventory.Count; i++)
            {
                //Example: "Heart" += "Key"
                inventoryText += Inventory[i].name + "\n"; //Adding the item names and separating the lines
            }

            GameManager.Instance.UImanager.ChangeInventoryText(inventoryText);
        }
        interactableObject.Interact();
    }


}
