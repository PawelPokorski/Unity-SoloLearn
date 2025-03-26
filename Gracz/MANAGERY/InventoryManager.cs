using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerInventoryManager : Manager
{
    protected List<ItemData> inventoryItems = new();

    public void AddItem(ItemData itemData)
    {
        inventoryItems.Add(itemData);
    }
}