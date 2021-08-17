using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Class responsible for inventory slots' behaviour. 
/// E.g. it keeps track of item stored inside and its amount. In case of player equipment it 
/// ensures that item is attempted to be put matches the type of intended items, which allows to set
/// sword/shield/helmet slot restrictions. Triggers two event which also ensure the functionality of 
/// equipment window by applying item buffs to the player and removing it.
/// </summary>
[Serializable]
public class InventorySlot
{
    public ItemType[] AllowedItems = new ItemType[0];
    [NonSerialized]
    public UserInterface parent;
    [NonSerialized]
    public GameObject slotDisplay;
    [NonSerialized]
    public SlotUpdated onAfterUpdate;
    [NonSerialized]
    public SlotUpdated onBeforeUpdate;
    public Item item;
    public int amount;

    /// <summary>
    /// Property locating the item in the database and links it to its scriptable object.
    /// </summary>
    public ItemObject ItemObject
    {
        get
        {
            if (item.Id >= 0)
            {
                return parent.inventory.database.itemObjects[item.Id];
            }
            else return null;
        }
    }
    /// <summary>
    /// Constructor for an empty slot.
    /// </summary>
    public InventorySlot()
    {
        UpdateSlot(new Item(), 0);
    }
    /// <summary>
    /// Constructor for a slot with _amount copies of _item inside.
    /// </summary>
    public InventorySlot(Item _item, int _amount)
    {
        UpdateSlot(_item, _amount);
    }
    /// <summary>
    /// Updates the contents of the slot with specified item and amount.
    /// </summary>
    /// <param name="_item">Put item.</param>
    /// <param name="_amount">Pute item's amount.</param>
    public void UpdateSlot(Item _item, int _amount)
    {
        onBeforeUpdate?.Invoke(this);
        item = _item;
        amount = _amount;
        onAfterUpdate?.Invoke(this);
    }
    /// <summary>
    /// Updates a slot to the empty state.
    /// </summary>
    public void RemoveItem()
    {
        UpdateSlot(new Item(), 0);
    }
    /// <summary>
    /// Adds the value to current item's amount.
    /// </summary>
    /// <param name="value">Added amount.</param>
    public void AddAmount(int value)
    {
        UpdateSlot(item, amount += value);
    }
    /// <summary>
    /// Subtracts the value from current item's amount. Removes item if changed amount is negative/zero.
    /// </summary>
    /// <param name="value">Removed amount.</param>
    public void DecreaseAmount(int value)
    {
        UpdateSlot(item, amount -= value);
        if (amount <= 0)
        {
            RemoveItem();
        }
    }
    /// <summary>
    /// Determines whether a specific items could be put in the slot. Used to set restrictions for equipment slots.
    /// </summary>
    /// <param name="_itemObject">Item being put.</param>
    /// <returns>True if item meets all slots' requirements, else otherwise.</returns>
    public bool CanPlaceInSlot(ItemObject _itemObject)
    {
        if (AllowedItems.Length <= 0 || _itemObject == null || _itemObject.data.Id < 0)
            return true;
        for (int i = 0; i < AllowedItems.Length; i++)
        {
            if (_itemObject.type == AllowedItems[i])
                return true;
        }
        return false;
    }

}
