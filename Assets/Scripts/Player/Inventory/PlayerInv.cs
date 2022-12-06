using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInv : MonoBehaviour
{
    public List<ItemData> inventory = new List<ItemData>();
    public int InvSize = 3;

    private void Start() { }

    private void Update() { }

    public bool AddItem(ItemData itemData) 
    {
        if (inventory.Count < InvSize) {
            inventory.Add(itemData);
            print("Item Added to inv" + itemData.itemDisplayName);
            return true;
        } else {
            return false;
        }
    }

    public void RemoveItem(ItemData itemData) {
        inventory.Remove(itemData);
    }

    public void increaseInvSize() {
        InvSize++;
    }

    public void decreaseInvSize() {
        InvSize--;
    }
}