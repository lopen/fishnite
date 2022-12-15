using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public List<InvSlot> inventorySlots = new List<InvSlot>(5); // Creates inv slot list
    public GameObject invSlotPrefab; // Assigns inv slot prefab
    public int invsize = 5; // Inv max size

    // Event listener for inv change
    private void OnEnable() {
        PlayerInv.OnInvChange += DrawInv;
    }

    // Event listener for inv change
    private void OnDisable() {
        PlayerInv.OnInvChange -= DrawInv;
    }
    
    // Reset inventory
    void ResetInv() {
        foreach(Transform childTransform in transform) {
            Destroy(childTransform.gameObject);
        }
        inventorySlots = new List<InvSlot>(invsize);
    }

    // Re-draws inventory
    void DrawInv(List<ItemData> playerInventory) {
        ResetInv();
        for (int i = 0; i < inventorySlots.Capacity; i++) {
            CreateNewInvSlot();
        }

        for(int i = 0; i < playerInventory.Count; i++) {
            inventorySlots[i].CreateInvSlot(playerInventory[i]);
        }
    }

    // Creates new inventory slot, not needed now with static inventory
    void CreateNewInvSlot() {
        GameObject newInvSlot = Instantiate(invSlotPrefab);
        newInvSlot.transform.SetParent(transform, false);

        InvSlot newSlotObj = newInvSlot.GetComponent<InvSlot>();
        newSlotObj.ClearInvSlot();

        inventorySlots.Add(newSlotObj);
    }
}
