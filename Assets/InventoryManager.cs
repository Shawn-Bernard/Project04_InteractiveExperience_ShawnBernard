using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public void AddToInventory(GameObject ItemToAdd)
    {
        foreach (GameObject item in GameManager.Instance.Inventory)
        {
            if (item.name == ItemToAdd.name)
            {
            Debug.Log("Item is already here");
            return;
            }
        }

        GameManager.Instance.Inventory.Add(ItemToAdd);
        GameManager.Instance.UImanager.ChangeInventoryText();
        Debug.Log($"Added new item {ItemToAdd.name} ");
    }

    public bool HasItemInInventory(GameObject item)
    {
        foreach (GameObject Item in GameManager.Instance.Inventory)
        {
            if (Item.name == item.name)
            {
                return true;
            }
        }
        return false;
    }
}
