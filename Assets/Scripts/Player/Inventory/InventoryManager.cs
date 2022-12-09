using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public List<InvSlot> inventorySlots = new List<InvSlot>(5);
    public GameObject invSlotPrefab;
    public int invsize = 5;

    private void OnEnable() {
        PlayerInv.OnInvChange += DrawInv;
    }

    private void OnDisable() {
        PlayerInv.OnInvChange -= DrawInv;
    }
    
    void ResetInv() {
        foreach(Transform childTransform in transform) {
            Destroy(childTransform.gameObject);
        }
        inventorySlots = new List<InvSlot>(invsize);
    }

    void DrawInv(List<ItemData> playerInventory) {
        ResetInv();
        for (int i = 0; i < inventorySlots.Capacity; i++) {
            CreateNewInvSlot();
        }

        for(int i = 0; i < playerInventory.Count; i++) {
            inventorySlots[i].CreateInvSlot(playerInventory[i]);
        }
    }

    void CreateNewInvSlot() {
        GameObject newInvSlot = Instantiate(invSlotPrefab);
        newInvSlot.transform.SetParent(transform, false);

        InvSlot newSlotObj = newInvSlot.GetComponent<InvSlot>();
        newSlotObj.ClearInvSlot();

        inventorySlots.Add(newSlotObj);
    }
}
