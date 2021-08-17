using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Basic class for the item. Stores item's name, id and buffs.
/// </summary>
[System.Serializable]
public class Item
{
    public string Name;
    public int Id = -1;
    public ItemBuff[] buffs;
    /// <summary>
    /// Constructor for an empty item;
    /// </summary>
    public Item()
    {
        Name = "";
        Id = -1;
    }
    /// <summary>
    /// Constructor for an item from itemObject.
    /// </summary>
    /// <param name="item">Item</param>
    public Item(ItemObject item)
    {
        Name = item.name;
        Id = item.data.Id;
        buffs = new ItemBuff[item.data.buffs.Length];
        for (int i = 0; i < buffs.Length; i++)
        {
            buffs[i] = new ItemBuff(item.data.buffs[i].min, item.data.buffs[i].max)
            {
                attribute = item.data.buffs[i].attribute
            };
        }
    }
    /// <summary>
    /// Auxiliarry method used to retrieve the index of specified attribute.
    /// </summary>
    /// <param name="_attribute">Sought attribute.</param>
    public int FetchBuffValue(Attributes _attribute)
    {
        foreach (var buff in buffs)
        {
            if (buff.attribute == _attribute)
            {
                return buff.value;
            }
        }
        return 0;
    }
}
