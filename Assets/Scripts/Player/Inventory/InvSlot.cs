using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InvSlot : MonoBehaviour
{
    public Image invIcon; // Inv slot icon
    public TextMeshProUGUI invItemName; // Inv slot name

    // Method for clearing inv slot
    public void ClearInvSlot() {
        invIcon.enabled = false;
        invItemName.enabled = false;
    }

    // Method for creating inv slot
    public void CreateInvSlot(ItemData itemData) {
        if(itemData == null) {
            ClearInvSlot();
            return;
        }

        invIcon.enabled = true;
        invItemName.enabled = true;
        invIcon.sprite = itemData.itemIcon;
        invItemName.text = itemData.itemDisplayName;
    }
}
