using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Class determining the behaviour of UI warning double-checking the intention to delete an item.
/// </summary>
public class WarningMessage : MonoBehaviour
{
    private DynamicInterface inventory;
    private StaticInterface equipment;
    private TextMeshProUGUI text;
    private void Awake()
    {
        inventory = FindObjectOfType<DynamicInterface>();
        equipment = FindObjectOfType<StaticInterface>();
        text = GetComponent<TextMeshProUGUI>();
    }
    private void OnEnable()
    {
        if (MouseData.lastInterface.GetComponent<DynamicInterface>() != null)
            text.text = "Are you sure you want to destroy " + inventory.lastSlot.item.Name +
            " ("+ inventory.lastSlot.amount+")"+ "?";
        else
            text.text = "Are you sure you want to destroy " + equipment.lastSlot.item.Name + "?";

    }
}
