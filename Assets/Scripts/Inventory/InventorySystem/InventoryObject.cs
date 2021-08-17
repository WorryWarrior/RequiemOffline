using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.IO;

public delegate void SlotUpdated(InventorySlot _slot);
/// <summary>
/// Enum allowing to identify item storage as inventory or equipment.
/// </summary>
public enum InterfaceType
{
    Inventory, 
    Equipment,
    //Banker :OO
}
/// <summary>
/// Note that whole inventory object system is built around scriptable objects (see https://docs.unity3d.com/Manual/class-ScriptableObject.html).
/// An object used as item flexible storage.
/// </summary>
[CreateAssetMenu(fileName = "New Inventory", menuName = "InventorySystem/Inventory")]
public class InventoryObject : ScriptableObject
{
    public ItemDatabaseObject database;
    public Inventory Container;
    public InterfaceType type;
    public InventorySlot[] GetSlots { get => Container.Slots; }
    /// <summary>
    /// Adds an item in inventory.
    /// </summary>
    /// <param name="_item">Added item.</param>
    /// <param name="_amount">Addet item's amount.</param>
    /// <returns>Was item added successfully or not.</returns>
    public bool AddItem(Item _item, int _amount)
    {
        if (EmptySlotCount <= 0)
            return false;
        InventorySlot slot = FindItemInInventory(_item);
        if (!database.itemObjects[_item.Id].isStackable || slot == null)
        {
            SetFirstEmptySlot(_item, _amount);
            return true;
        }
        slot.AddAmount(_amount);
        return true;
    }
    /// <summary>
    /// Returns the index of the first empty slot.
    /// </summary>
    public int EmptySlotCount
    {
        get
        {
            int counter = 0;
            for (int i = 0; i < GetSlots.Length; i++)
            {
                if (GetSlots[i].item.Id <= -1)
                    counter++;
            }
            return counter;
        }
    }
    /// <summary>
    /// Checks item's presence in inventory.
    /// </summary>
    /// <param name="_item">Item to be sought.</param>
    /// <returns>Item's inventory index in case of success or null otherwise.</returns>
    public InventorySlot FindItemInInventory(Item _item)
    {
        for (int i = 0; i < GetSlots.Length; i++)
        {
            if (GetSlots[i].item.Id == _item.Id)
                return GetSlots[i];
        }
        return null;
    }
    /// <summary>
    /// Puts n copies of the item in first inventory slot. 
    /// </summary>
    /// <param name="_item">Item to be put in slot.</param>
    /// <param name="amount">Amount of items to be put in slot.</param>
    /// <returns>Changed slot.</returns>
    public InventorySlot SetFirstEmptySlot(Item _item, int amount)
    {
        for (int i = 0; i < GetSlots.Length; i++)
        {
            if (GetSlots[i].item.Id <= -1)
            {
                GetSlots[i].UpdateSlot(_item, amount);
                return GetSlots[i];
            }
        }
        // Inventory is full.
        return null;
    }
    /// <summary>
    /// Swaps two slots of the inventory.
    /// </summary>
    /// <param name="item1">First slot to swap.</param>
    /// <param name="item2">Second slot to swap.</param>
    public void SwapItems(InventorySlot item1, InventorySlot item2)
    {
        if (item2.CanPlaceInSlot(item1.ItemObject) && item1.CanPlaceInSlot(item2.ItemObject))
        {
            InventorySlot temp = new InventorySlot(item2.item, item2.amount);
            item2.UpdateSlot(item1.item, item1.amount);
            item1.UpdateSlot(temp.item, temp.amount);
        }
        
    }
    /// <summary>
    /// Removes specified item from inventory.
    /// </summary>
    /// <param name="_item">Item to remove.</param>
    public void RemoveItem(Item _item)
    {
        for (int i = 0; i < GetSlots.Length; i++)
        {
            if (GetSlots[i].item == _item)
            {
                GetSlots[i].UpdateSlot(null, 0);
            }
        }
    }
    /// <summary>
    /// Saves contents of inventory through serialization. Note that due to deserialization issues after saving contents of two different Inventory Objects 
    /// in a single .bf file there are two identical methods, each for its own Inventory Object. 
    /// </summary>
    [ContextMenu("SaveInventory")]
    public void SaveInventory()
    {
        IFormatter formatter = new BinaryFormatter();
        FileStream file = new FileStream(GetDataPath(), FileMode.OpenOrCreate);
        formatter.Serialize(file, Container);
        file.Close();
    }
    /// <summary>
    /// Loads the contents of inventory through deserialization.
    /// </summary>
    [ContextMenu("LoadInventory")]
    public void LoadInventory()
    {
        if (File.Exists(GetDataPath()))
        {
            IFormatter formatter = new BinaryFormatter();
            FileStream file = new FileStream(GetDataPath(), FileMode.Open);
            Inventory proxyContainer = (Inventory)formatter.Deserialize(file);
            for (int i = 0; i < GetSlots.Length; i++)
            {
                GetSlots[i].UpdateSlot(proxyContainer.Slots[i].item, proxyContainer.Slots[i].amount);
            }
            file.Close();
        }
    }
    [ContextMenu("SaveEquipment")]
    public void SaveEquipment()
    {
        IFormatter formatter = new BinaryFormatter();
        FileStream file = new FileStream(GetSecondDataPath(), FileMode.OpenOrCreate);
        formatter.Serialize(file, Container);
        file.Close();
    }
    [ContextMenu("LoadEquipment")]
    public void LoadEquipment()
    {
        if (File.Exists(GetSecondDataPath()))
        {
            IFormatter formatter = new BinaryFormatter();
            FileStream file = new FileStream(GetSecondDataPath(), FileMode.Open);
            Inventory proxyContainer = (Inventory)formatter.Deserialize(file);
            for (int i = 0; i < GetSlots.Length; i++)
            {
                GetSlots[i].UpdateSlot(proxyContainer.Slots[i].item, proxyContainer.Slots[i].amount);
            }
            file.Close();
        }
    }
    /// <summary>
    /// Returns a path to .bf file for storing the data.
    /// </summary>
    public string GetDataPath()
    {
        return Application.persistentDataPath + "/data.bf";
    }
    public string GetSecondDataPath()
    {
        return Application.persistentDataPath + "/data1.bf";
    }
    /// <summary>
    /// Clears the contents of Inventory Object.
    /// </summary>
    [ContextMenu("Clear")]
    public void Clear()
    {
        for (int i = 0; i < GetSlots.Length; i++)
        {
            GetSlots[i].RemoveItem();
        }
    }
}


