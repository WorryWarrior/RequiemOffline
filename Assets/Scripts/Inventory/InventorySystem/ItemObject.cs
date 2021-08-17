using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Enum used to distinguish items by their types which determine their behaviour and applied UI approach.
/// </summary>
public enum ItemType
{
    Helmet,
    Body,
    Shield,
    Sword,
    Boots,
    Potion
}
/// <summary>
/// Attribute Potions restore.
/// </summary>
public enum RestoredAttribute
{
    Health,
    Mana
}
/// <summary>
/// Attributes applied to items.
/// </summary>
public enum Attributes
{
    Constitution,
    Intellect,
    Mind
}
/// <summary>
/// A basic Scriptable Object representing the item. Stores all data about the item.
/// </summary>
public abstract class ItemObject : ScriptableObject
{
    public Sprite uiSprite;
    public ItemType type;
    public string description;
    public Item data = new Item();
    public bool isStackable = true;

    public float restoredValue = 0;
    public RestoredAttribute attribute;
    public Item CreateItem()
    {
        Item newItem = new Item(this);
        return newItem;
    }
}
/// <summary>
/// Class representing a buff applied 
/// </summary>
[System.Serializable]
public class ItemBuff : IModifier
{
    public Attributes attribute;
    public int value;
    public int max;
    public int min;
    /// <summary>
    /// Constructor for a buff with a random value between given constraints.
    /// </summary>
    /// <param name="_min">Minimum constraint.</param>
    /// <param name="_max">Maximum constraint.</param>
    public ItemBuff(int _min, int _max)
    {
        min = _min;
        max = _max;
        GenerateValue();
    }
    /// <summary>
    /// Adds a value to the base value.
    /// </summary>
    /// <param name="baseValue"></param>
    public void AddValue(ref int baseValue)
    {
        baseValue += value;
    }
    /// <summary>
    /// Generates a random value between min inclusively and max exclusively.
    /// </summary>
    public void GenerateValue()
    {
        value = Random.Range(min, max);
    }
}
