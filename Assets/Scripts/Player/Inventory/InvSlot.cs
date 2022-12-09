using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InvSlot : MonoBehaviour
{
    public Image invIcon;
    public TextMeshProUGUI invItemName;

    public void ClearInvSlot() {
        invIcon.enabled = false;
        invItemName.enabled = false;
    }

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
    // Update is called once per frame
    void Update()
    {
        
    }
}
