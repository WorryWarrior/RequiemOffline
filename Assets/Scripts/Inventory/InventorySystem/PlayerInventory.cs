using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public InventoryObject inventory;
    public InventoryObject equipment;
    public Attribute[] attributes;

    /// <summary>
    /// Subscribes two main methods to each slot's event in order to ensure attribute buff system functionality.
    /// </summary>
    private void Start()
    {
        for (int i = 0; i < attributes.Length; i++)
        {
            attributes[i].SetParent(this);
        }
        for (int i = 0; i < equipment.GetSlots.Length; i++)
        {
            equipment.GetSlots[i].onBeforeUpdate += OnBeforeSlotUpdate;
            equipment.GetSlots[i].onAfterUpdate += OnAfterSlotUpdate;
        }

    }
    /// <summary>
    /// Applies attribute buffs when target slot is the part of equipment window,
    /// does nothing when target slot is the part of inventory.
    /// </summary>
    /// <param name="_slot">Target slot.</param>
    public void OnBeforeSlotUpdate(InventorySlot _slot)
    {
        if (_slot.ItemObject == null)
            return;
        switch (_slot.parent.inventory.type)
        {
            case InterfaceType.Inventory:
                break;
            case InterfaceType.Equipment:
                for (int i = 0; i < _slot.item.buffs.Length; i++)
                {
                    for (int j = 0; j < attributes.Length; j++)
                    {
                        if (attributes[j].type == _slot.item.buffs[i].attribute)
                        {
                            attributes[j].value.RemoveModifier(_slot.item.buffs[i]);
                            GetComponent<PlayerHealth>().EqualizeHealth();
                            GetComponent<PlayerMana>().EqualizeMana();
                        }
                    }
                }
                break;
            default:
                break;
        }
        UpdateCounters();
    }
    /// <summary>
    /// Removes attribute applied buffs when target slot is the part of equipment window,
    /// does nothing when target slot is the part of inventory.
    /// </summary>
    /// <param name="_slot">Target slot.</param>
    public void OnAfterSlotUpdate(InventorySlot _slot)
    {
        if (_slot.ItemObject == null)
            return;
        switch (_slot.parent.inventory.type)
        {
            case InterfaceType.Inventory:
                break;
            case InterfaceType.Equipment:
                for (int i = 0; i < _slot.item.buffs.Length; i++)
                {
                    for (int j = 0; j < attributes.Length; j++)
                    {
                        if (attributes[j].type == _slot.item.buffs[i].attribute)
                        {
                            attributes[j].value.AddModifier(_slot.item.buffs[i]);
                            GetComponent<PlayerHealth>().UpdateHealthUI();
                        }
                    }
                }
                break;
            default:
                break;
        }
        UpdateCounters();
    }
    /// <summary>
    /// Auxiliary method to update UI player stat trackers.
    /// </summary>
    public void UpdateCounters()
    {
        if (CONCounter.Instance != null && MNDCounter.Instance != null && INTCounter.Instance != null)
        {
            CONCounter.Instance.UpdateConstitutionCounter();
            MNDCounter.Instance.UpdateMindCounter();
            INTCounter.Instance.UpdateIntellectCounter();
        }
    }
    /// <summary>
    /// Determines the behavour of three primary attributes.
    /// Constitution increases max HP by 1 per point.
    /// Intellect increases max MP by 1 per point.
    /// Mind increases experience granted by 1% per point.
    /// </summary>
    public void AttributeModified(Attribute attribute)
    {
        PlayerHealth health = GetComponent<PlayerHealth>();
        PlayerMana mana = GetComponent<PlayerMana>();
        health.ModifyMaxHealth(attributes[GetAttributeID(Attributes.Constitution)].value.ModifiedValue);
        mana.ModifyMaxMana(attributes[GetAttributeID(Attributes.Intellect)].value.ModifiedValue);
        Experience.Instance.ChangeExperienceModifier((float)attributes[GetAttributeID(Attributes.Mind)].value.ModifiedValue / 100);
        Debug.Log(attribute.type+ " was updated. New value is " +attribute.value.ModifiedValue);
    }
    /// <summary>
    /// Auxiliary method for fetching ID of certain attribute from array.
    /// </summary>
    /// <param name="_attribute">Sought attribute</param>
    /// <returns>Attribute ID</returns>
    public int GetAttributeID(Attributes _attribute)
    {
        for (int i = 0; i < attributes.Length; i++)
        {
            if (attributes[i].type == _attribute)
                return i;
        }
        return -1;
    }
    /// <summary>
    /// Removes contents from inventory and equipment.
    /// </summary>
    private void OnApplicationQuit()
    {
        inventory.Clear();
        equipment.Clear();
        
    }
}
