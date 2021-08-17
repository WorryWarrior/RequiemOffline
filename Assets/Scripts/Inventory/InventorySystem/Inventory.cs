using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Class representing player's inventory structure. Used for data serialization/deserialization casting.
/// </summary>
[Serializable]
public class Inventory
{
    public InventorySlot[] Slots = new InventorySlot[20];
    /// <summary>
    /// Replaces all stored items with zero amount of default one.
    /// </summary>
    public void Clear()
    {
        for (int i = 0; i < Slots.Length; i++)
        {
            Slots[i].UpdateSlot(new Item(), 0);
        }
    }
}
