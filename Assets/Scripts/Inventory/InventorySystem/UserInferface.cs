using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.Events;

/// <summary>
/// Main class of system UI. Used to define callbacks.
/// </summary>
public abstract class UserInterface : MonoBehaviour
{
    public InventoryObject inventory;
    public BagButton button;
    public Image destroyWarning;
    [HideInInspector] public bool itemIsChanged = true;
    [HideInInspector] public InventorySlot lastSlot;
    public ItemPopup popup;
    public Dictionary<GameObject, InventorySlot> slotsOnInterface = new Dictionary<GameObject, InventorySlot>();
    /// <summary>
    /// Creates slots and defines two callbacks responsible for tracking the interface mouse is over.
    /// </summary>
    private void Start()
    {
        for (int i = 0; i < inventory.GetSlots.Length; i++)
        {
            inventory.GetSlots[i].parent = this;
            inventory.GetSlots[i].onAfterUpdate += OnSlotUpdate;
        }
        CreateSlots();
        AddEvent(gameObject, EventTriggerType.PointerEnter, delegate { OnInterfaceEnter(gameObject); });
        AddEvent(gameObject, EventTriggerType.PointerExit, delegate { OnInterfaceExit(gameObject); });
        gameObject.SetActive(false);
    }
    /// <summary>
    /// Updates the state of a single Inventory Slot.
    /// </summary>
    /// <param name="_slot">Slot to update.</param>
    public void OnSlotUpdate(InventorySlot _slot)
    {
        if (_slot.item.Id >= 0) //Has an item inside
        {
            _slot.slotDisplay.transform.GetChild(0).GetComponentInChildren<Image>().sprite = _slot.ItemObject.uiSprite;
            _slot.slotDisplay.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
            _slot.slotDisplay.GetComponentInChildren<TextMeshProUGUI>().text = _slot.amount == 1 ? "" : _slot.amount.ToString("n0");
        }
        else
        {
            _slot.slotDisplay.transform.GetChild(0).GetComponentInChildren<Image>().sprite = null;
            _slot.slotDisplay.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0);
            _slot.slotDisplay.GetComponentInChildren<TextMeshProUGUI>().text = "";
        }
    }

    public abstract void CreateSlots();
    /// <summary>
    /// Updates all existing slots.
    /// </summary>
    public void UpdateSlots()
    {
        foreach (KeyValuePair<GameObject, InventorySlot> _slot in slotsOnInterface)
        {
            if (_slot.Value.item.Id >= 0) //Has an item inside
            {
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = _slot.Value.ItemObject.uiSprite;
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
                _slot.Key.GetComponentInChildren<TextMeshProUGUI>().text = _slot.Value.amount == 1 ? "" : _slot.Value.amount.ToString("n0");
            }
            else
            {
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = null;
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0);
                _slot.Key.GetComponentInChildren<TextMeshProUGUI>().text = "";
            }
        }
    }
    /// <summary>
    /// Auxiliary method used to set up callbacks.
    /// </summary>
    public void AddEvent(GameObject obj, EventTriggerType type, UnityAction<BaseEventData> action)
    {
        EventTrigger trigger = obj.GetComponent<EventTrigger>();
        EventTrigger.Entry eventTrigger = new EventTrigger.Entry
        {
            eventID = type
        };
        eventTrigger.callback.AddListener(action);
        trigger.triggers.Add(eventTrigger);
    }
    private int clicked = 0;
    private float clickTime = 0;
    private float doubleClickTime = 0.5f;
    /// <summary>
    /// Determines the behaviour of clicked slot. Restores certain attribute if clicked slot contained a potion, doesn't do anything otherwise.
    /// </summary>
    public void OnClick(GameObject obj)
    {
        if (MouseData.slotHoveredOver != null && slotsOnInterface[MouseData.slotHoveredOver].ItemObject != null 
            && slotsOnInterface[MouseData.slotHoveredOver].ItemObject.type == ItemType.Potion)
        {
            clicked++;
            if (clicked == 1)
                clickTime = Time.time;
            if (clicked == 2 && Time.time - clickTime < doubleClickTime)
            {
                // ADD POTION USE SOUND HERE.
                if (slotsOnInterface[MouseData.slotHoveredOver].ItemObject.attribute == RestoredAttribute.Health)
                {
                    FindObjectOfType<PlayerHealth>().GainHealth(slotsOnInterface[MouseData.slotHoveredOver].ItemObject.restoredValue);
                }
                else
                {
                    FindObjectOfType<PlayerMana>().RestoreMana(slotsOnInterface[MouseData.slotHoveredOver].ItemObject.restoredValue);
                }
                slotsOnInterface[MouseData.slotHoveredOver].DecreaseAmount(1);
                UpdatePopup();
                clicked = 0;
            }
            else if (clicked > 2 || Time.time - clickTime > 1)
                clicked = 0;
        }

    }
    /// <summary>
    /// Determines the behaviour of enter slot callback. Displays item popup in different positions according to the interface entered slot belongs to.
    /// </summary>
    /// <param name="obj"></param>
    public void OnEnter(GameObject obj)
    {
        MouseData.slotHoveredOver = obj;
        if (MouseData.interfaceMouseIsOver != null)
        {
            if (MouseData.interfaceMouseIsOver.GetComponent<DynamicInterface>() != null)
                popup.UpdatePosition(MouseData.slotHoveredOver.GetComponent<RectTransform>().anchoredPosition, 225, 30);
            else
                popup.UpdatePosition(MouseData.slotHoveredOver.GetComponent<RectTransform>().anchoredPosition, -225, 60);
        }
        if (itemIsChanged)
            lastSlot = slotsOnInterface[MouseData.slotHoveredOver];
        UpdatePopup();
    }
    /// <summary>
    /// Determines the behaviour of exit slot callback. Hides item popup.
    /// </summary>
    public void OnExit(GameObject obj)
    {
        MouseData.slotHoveredOver = null;
        popup.SetComponentsInvisible();
    }
    /// <summary>
    /// Determines the behaviour of drag start callback.
    /// </summary>
    /// <param name="obj"></param>
    public void OnDragStart(GameObject obj)
    {
        MouseData.tempItemBeingDragged = CreateTempItem(obj);
        itemIsChanged = false;
        MouseData.interfaceIsChanged = false;
    }
    /// <summary>
    /// Creates a temporary image of an item stored in specified slot.
    /// </summary>
    /// <param name="obj">Item slot containing an item.</param>
    /// <returns>Temp image of a dragged item</returns>
    public GameObject CreateTempItem(GameObject obj)
    {
        GameObject tempItem = null;
        if (slotsOnInterface[obj].item.Id >= 0) // Has an item on it
        {
            tempItem = new GameObject();
            var rt = tempItem.AddComponent<RectTransform>();
            rt.sizeDelta = new Vector2(30 * FindObjectOfType<Canvas>().scaleFactor, 30 * FindObjectOfType<Canvas>().scaleFactor);
            tempItem.transform.SetParent(transform.parent);
            var image = tempItem.AddComponent<Image>();
            image.sprite = slotsOnInterface[obj].ItemObject.uiSprite;
            image.raycastTarget = false;
        }
        return tempItem;
    }
    /// <summary>
    /// Determines the behaviour of drag slot end callback. Enables UI window double-checking whether item should be destroyed
    /// when non-null item is attempted to be destroyed.
    /// </summary>
    /// <param name="obj"></param>
    public void OnDragEnd(GameObject obj)
    {
        itemIsChanged = true;
        MouseData.interfaceIsChanged = true;
        Destroy(MouseData.tempItemBeingDragged);
        if (MouseData.interfaceMouseIsOver == null && lastSlot.item.Name != "")
        {
            destroyWarning.gameObject.SetActive(true);
            //slotsOnInterface[obj].RemoveItem();
            return;
        }
        if (MouseData.slotHoveredOver)
        {
            InventorySlot mouseHoverSlotData = MouseData.interfaceMouseIsOver.slotsOnInterface[MouseData.slotHoveredOver];
            inventory.SwapItems(slotsOnInterface[obj], mouseHoverSlotData);
        }
    }
    /// <summary>
    /// Determines the behaviour of drag slot callback. Ensures temp item image position is matching current cursor position.
    /// </summary>
    /// <param name="obj"></param>
    public void OnDrag(GameObject obj)
    {
        if (MouseData.tempItemBeingDragged != null)
        {
            MouseData.tempItemBeingDragged.GetComponent<RectTransform>().position = Input.mousePosition;
        }
    }
    /// <summary>
    /// Determines the behaviour of enter interface callback.
    /// </summary>
    /// <param name="obj"></param>
    public void OnInterfaceEnter(GameObject obj)
    {
        MouseData.interfaceMouseIsOver = obj.GetComponent<UserInterface>();
        if (MouseData.interfaceIsChanged)
            MouseData.lastInterface = obj.GetComponent<UserInterface>();
    }
    /// <summary>
    /// Determines the behaviour of exit interface callback.
    /// </summary>
    /// <param name="obj"></param>
    public void OnInterfaceExit(GameObject obj)
    {
        MouseData.interfaceMouseIsOver = null;
    }
    /// <summary>
    /// Update skill popup according to the item contained in the slot mouse is currently over.
    /// </summary>
    private void UpdatePopup()
    {
        if (slotsOnInterface[MouseData.slotHoveredOver].item.Name != "")
        {
                popup.SetComponentsVisible(slotsOnInterface[MouseData.slotHoveredOver].ItemObject,
                    slotsOnInterface[MouseData.slotHoveredOver].item.Name,
                    slotsOnInterface[MouseData.slotHoveredOver].ItemObject.uiSprite,
                    slotsOnInterface[MouseData.slotHoveredOver].item.FetchBuffValue(Attributes.Constitution),
                    slotsOnInterface[MouseData.slotHoveredOver].item.FetchBuffValue(Attributes.Intellect),
                    slotsOnInterface[MouseData.slotHoveredOver].item.FetchBuffValue(Attributes.Mind));
            
        }
        else
        {
            popup.SetComponentsInvisible();
        }
    }
    /// <summary>
    /// Close UI window. Used from buttons.
    /// </summary>
    public void CloseUIWindow()
    {
        gameObject.SetActive(false);
        button.button.image.sprite = button.closed;
        button.switcher = !button.switcher;
    }
    /// <summary>
    /// Open or close UI window depending on its current state. Used from buttons.
    /// </summary>
    public void OpenUIWindow()
    {
        if (gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }
}
