using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "Fishnite/ItemData", order = 0)]
public class ItemData : ScriptableObject {
    public string itemDisplayName;
    public Sprite itemIcon;
}
