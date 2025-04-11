using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] public List<GameObject> Inventory;

    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameObject Current = null;
    GameObject Interactable;

    [SerializeField] private InteractableObject interactableObject;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Current Inventory Count: " + Inventory.Count);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {   
        
        if (other.gameObject.CompareTag("Interactable"))
        {
            Debug.Log("Here");
            Current = other.gameObject;
            PlayerInputActions.InteractEvent += Interact;
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

        interactableObject.Interact();
    }

    public void AddToInventory(GameObject Item)
    {
        if (Inventory.Contains(Item))
        {
            Debug.Log("Item already in inventory: " + Item.name);
            return;
        }
        else
        {
            GameObject newItem = Item;

            Inventory.Add(newItem);
            Inventory.Add(Item);

            Debug.Log("Item added to inventory: " + Item.name); // Log this

            GameManager.Instance.UImanager.ChangeInventoryText();
        }
    }

    public bool HasItemInInventory(GameObject item)
    {
        foreach (GameObject Item in Inventory)
        {
            if (Item.name == item.name)
            {
                return true;
            }
        }
        return false;
    }


}
