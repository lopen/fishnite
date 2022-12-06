using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class InventoryItem
{
    public ItemData itemData;
  
    public InventoryItem (ItemData item) 
    {
        itemData = item;
    }
}
