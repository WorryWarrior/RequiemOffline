using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class determining the behaviour of UI window double-checking the intention to delete an item.
/// </summary>
public class WarningWindow : MonoBehaviour
{
    private DynamicInterface inventory;
    private StaticInterface equipment;
    private void Awake()
    {
        inventory = FindObjectOfType<DynamicInterface>();
        equipment = FindObjectOfType<StaticInterface>();
    }
    /// <summary>
    /// Closes the UI window.
    /// </summary>
    public void CloseWindow()
    {
        gameObject.SetActive(false);
        MouseData.interfaceIsChanged = true;
    }
    /// <summary>
    /// Destroys last non-empty dragged item.
    /// </summary>
    public void DestroyItem()
    {
        Debug.Log(MouseData.lastInterface.name);
        if (MouseData.lastInterface.GetComponent<DynamicInterface>() != null)
        {
            for (int i = 0; i < inventory.inventory.Container.Slots.Length; i++)
            {
                if (inventory.inventory.Container.Slots[i] == inventory.lastSlot)
                {
                    inventory.lastSlot.RemoveItem();
                }
            }
        }
        else
        {
            for (int i = 0; i < equipment.inventory.Container.Slots.Length; i++)
            {
                if (equipment.inventory.Container.Slots[i] == equipment.lastSlot)
                {
                    equipment.lastSlot.RemoveItem();
                }
            }
        }
        CloseWindow();
    }
}
