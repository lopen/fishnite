using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "Fishnite/ItemData", order = 0)] // Asset menu creation shortcut
public class ItemData : ScriptableObject {
    public string itemDisplayName; // Item display name
    public Sprite itemIcon; // Item icon (sprite)
    public float weight; // Item weight (point amount)
}
