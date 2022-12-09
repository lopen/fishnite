using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInv : MonoBehaviour
{
    public static event Action<List<ItemData>> OnInvChange;

    public int invSize = 5;
    public List<ItemData> inventory = new List<ItemData>();
    

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

    public void RemoveItem(ItemData itemData) {
        inventory.Remove(itemData);
        OnInvChange?.Invoke(inventory);
    }

    public void increaseInvSize() {
        invSize++;
    }

    public void decreaseInvSize() {
        invSize--;
    }
}