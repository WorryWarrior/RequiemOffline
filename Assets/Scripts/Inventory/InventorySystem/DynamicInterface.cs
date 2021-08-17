using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Class responsible for generating player inventory callbacks and correct positioning of item slots.
/// </summary>
public class DynamicInterface : UserInterface
{
    public GameObject inventoryPrefab;
    public int columnAmount;
    public int xOffset;
    public int yOffset;
    public int initialX;
    public int initialY;
    public override void CreateSlots()
    {
        slotsOnInterface = new Dictionary<GameObject, InventorySlot>();
        for (int i = 0; i < inventory.Container.Slots.Length; i++)
        {
            var obj = Instantiate(inventoryPrefab, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
            AddEvent(obj, EventTriggerType.PointerEnter, delegate { OnEnter(obj); });
            AddEvent(obj, EventTriggerType.PointerExit, delegate { OnExit(obj); });
            AddEvent(obj, EventTriggerType.BeginDrag, delegate { OnDragStart(obj); });
            AddEvent(obj, EventTriggerType.EndDrag, delegate { OnDragEnd(obj); });
            AddEvent(obj, EventTriggerType.Drag, delegate { OnDrag(obj); });
            AddEvent(obj, EventTriggerType.PointerDown, delegate { OnClick(obj); });
            inventory.GetSlots[i].slotDisplay = obj;
            slotsOnInterface.Add(obj, inventory.Container.Slots[i]);
        }
    }
    /// <summary>
    /// Determines the correct position of each item slot within inventory UI.
    /// </summary>
    private Vector3 GetPosition(int i)
    {
        return new Vector3(initialX + (xOffset * (i % columnAmount)), initialY + (-yOffset * (i / columnAmount)), 0f);
    }
}
