using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInv : MonoBehaviour
{
    public static event Action<List<ItemData>> OnInvChange; // Event handler for on inv change

    public int invSize = 5; // Public int size definition
    public List<ItemData> inventory = new List<ItemData>(); // item data list for inventory
    

    // Method for adding item (fish) to inventory
    public bool AddItem(ItemData itemData) 
    {
        if (inventory.Count < invSize) {
            inventory.Add(itemData);
            print("Item Added to inv" + itemData.itemDisplayName);
            OnInvChange?.Invoke(inventory);
            return true;
        } else {
            return false;
        }
    }

    // Method for removing items from inventory.
    public void RemoveItem(ItemData itemData) {
        inventory.Remove(itemData);
        OnInvChange?.Invoke(inventory);
    }

    // Method for increasing inv size (no longer needed)
    public void increaseInvSize() {
        invSize++;
    }

    // Method for decreasing inv size (no longer needed, possibly shark attack will lower inv size/max health in the future)
    public void decreaseInvSize() {
        invSize--;
    }
}