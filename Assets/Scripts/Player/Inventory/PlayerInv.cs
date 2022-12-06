using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInv : MonoBehaviour
{
    public List<InventoryItem> inventory = new List<InventoryItem>();
    public int InvSize = 3;
    [SerializeField] public ItemData fishTester;

    private void Start() {
        //InventoryItem newItem = new InventoryItem(fishTester);
        //MinigameFunc.OnFishWin += AddItemSpecificFish;
    }

    private void Update() {
        //MinigameFunc.OnFishWin += AddItemSpecificFish;
    }
    public void AddItem(ItemData itemData) {
        InventoryItem newItem = new InventoryItem(itemData);
        inventory.Add(newItem);
        print("Item Added to inv" + itemData.itemDisplayName);
    }

    public void RemoveItem(ItemData itemData) {
        //inventory.Remove(itemData);
    }

    public void AddItemBigFish() {
        //InventoryItem newItem = new InventoryItem(fishTester);
        //inventory.Add(newItem);
        print("Item Added to inv");
    }
}