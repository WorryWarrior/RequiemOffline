using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Static class used to track mouse position via callbacks.
/// </summary>
public static class MouseData
{
    public static UserInterface lastInterface;
    public static bool interfaceIsChanged = true;

    public static UserInterface interfaceMouseIsOver;
    public static GameObject tempItemBeingDragged;
    public static GameObject slotHoveredOver;

    public static SkillUserInterface skillInterfaceMouseIsOver;
    public static GameObject tempSkillBeingDragged;
    public static GameObject skillSlotHoveredOver;
}
