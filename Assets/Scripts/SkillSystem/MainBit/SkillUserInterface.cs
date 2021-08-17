using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using UnityEngine.EventSystems;

/// <summary>
/// A set of various methods/variables/components used both by two descendants: SkillBar.cs and SkillWindow.cs.
/// </summary>
public abstract class SkillUserInterface : MonoBehaviour
{
    public SkillInventoryObject inventory;
    public SkillInventoryObject bar;
    public BagButton button;
    public static SkillSlot lastSkillSlot;
    public GameObject tempSkillPrefab;
    public Dictionary<GameObject, SkillSlot> skillsOnInterface = new Dictionary<GameObject, SkillSlot>();
    [HideInInspector] public bool skillIsDragged;
    public AudioClip slotSwap;
    public System.Action onSkillLevelledUp;

    /// <summary>
    /// Creates the slots, adds two callbacks to track current interface.
    /// </summary>
    public virtual void Start()
    {
        for (int i = 0; i < inventory.GetSlots.Length; i++)
        {
            inventory.GetSlots[i].parent = this;
            inventory.GetSlots[i].onAfterUpdate += OnSkillSlotUpdate;
        }
        CreateSlots();
        UpdateSlots();
        AddEvent(gameObject, EventTriggerType.PointerEnter, delegate { OnSkillInterfaceEnter(gameObject); });
        AddEvent(gameObject, EventTriggerType.PointerExit, delegate { OnSkillInterfaceExit(gameObject); });
    }
    /// <summary>
    /// Updates UI components of a single slot.
    /// </summary>
    /// <param name="_slot">Updated slot.</param>
    public void OnSkillSlotUpdate(SkillSlot _slot)
    {
        if (_slot.skill.Id >= 0) //Has a skill inside
        {
            //_slot.slotDisplay.transform.GetChild(0).GetComponentInChildren<Image>().sprite = _slot.SkillObject.activeSprite;
            if (_slot.SkillObject.level > 0)
                _slot.slotDisplay.transform.GetChild(0).GetComponentInChildren<Image>().sprite = _slot.SkillObject.activeSprite;
            else
                _slot.slotDisplay.transform.GetChild(0).GetComponentInChildren<Image>().sprite = _slot.SkillObject.inactiveSprite;
            _slot.slotDisplay.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
            _slot.slotDisplay.GetComponentInChildren<TextMeshProUGUI>().text =
                _slot.SkillObject.level == 0 ? "" : _slot.SkillObject.level.ToString("n0");
        }
        else
        {
            _slot.slotDisplay.transform.GetChild(0).GetComponentInChildren<Image>().sprite = null;
            _slot.slotDisplay.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0);
            _slot.slotDisplay.GetComponentInChildren<TextMeshProUGUI>().text = "";

        }
    }
    /// <summary>
    /// Creates slots. Two descendants don't have any shared functionality.
    /// </summary>
    public abstract void CreateSlots();
    /// <summary>
    /// Updates UI components of all existing slots.
    /// </summary>
    public void UpdateSlots()
    {
        foreach (KeyValuePair<GameObject, SkillSlot> _slot in skillsOnInterface)
        {
            if (_slot.Value.skill.Id >= 0) //Has an item inside
            {
                if (_slot.Value.SkillObject.level > 0)
                    _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = _slot.Value.SkillObject.activeSprite;
                else
                    _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = _slot.Value.SkillObject.inactiveSprite;
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
                _slot.Key.GetComponentInChildren<TextMeshProUGUI>().text =
                    _slot.Value.SkillObject.level == 0 ? "" : _slot.Value.SkillObject.level.ToString("n0");
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
    /// Auxiliary method to create a callback.
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
    /// <summary>
    /// Keeps track of last non-null interacted slots.
    /// </summary>
    /// <param name="obj">Clicked slot.</param>
    public virtual void OnSkillClick(GameObject obj)
    {
        if (!skillIsDragged && MouseData.skillInterfaceMouseIsOver != null && MouseData.skillSlotHoveredOver != null)
        {
            lastSkillSlot = MouseData.skillInterfaceMouseIsOver.skillsOnInterface[MouseData.skillSlotHoveredOver];
        }
    }
    /// <summary>
    /// Keeps track of the slot mouse is over.
    /// </summary>
    /// <param name="obj">Entered slot.</param>
    public virtual void OnSkillEnter(GameObject obj)
    {
        MouseData.skillSlotHoveredOver = obj;
    }
    /// <summary>
    /// Keeps track of the slot mouse is over.
    /// </summary>
    /// <param name="obj">Exited slot.</param>
    public virtual void OnSkillExit(GameObject obj)
    {
        MouseData.skillSlotHoveredOver = null;
    }
    /// <summary>
    /// Determines whether skill attempted to drag is eligible to be dragged.
    /// Creates a temporary copy in case of success.
    /// </summary>
    /// <param name="obj">Dragged slot.</param>
    public void OnSkillDragStart(GameObject obj)
    {
        if (lastSkillSlot != null && lastSkillSlot.SkillObject != null && lastSkillSlot.SkillObject.level != 0 &&
          lastSkillSlot.SkillObject.type != SkillType.Passive)
            MouseData.tempSkillBeingDragged = CreateTempSkill(obj);
    }
    /// <summary>
    /// Creates a temporary game object.
    /// </summary>
    /// <param name="obj">Original skill slot.</param>
    /// <returns>Temporary Game Object with adjusted Image component.</returns>
    public GameObject CreateTempSkill(GameObject obj)
    {
        skillIsDragged = true;
        GameObject tempSkill = null;
        if (skillsOnInterface[obj].skill.Id >= 0) // Has an skill on it
        {
            var canvas = FindObjectOfType<Canvas>();
            tempSkill = Instantiate(tempSkillPrefab, obj.transform.position, Quaternion.identity);
            tempSkill.transform.SetParent(transform.parent);
            tempSkill.GetComponent<RectTransform>().sizeDelta = new Vector2(30 * canvas.scaleFactor, 30 * canvas.scaleFactor);
            var image = tempSkill.GetComponent<Image>();
            image.sprite = skillsOnInterface[obj].SkillObject.activeSprite;
            image.raycastTarget = false;
        }
        return tempSkill;
    }
    /// <summary>
    /// Copies a skill to another slot.
    /// </summary>
    /// <param name="skill1">First slot.</param>
    /// <param name="skill2">Second slot.</param>
    public void CopySkill(SkillSlot skill1, SkillSlot skill2)
    {
        if (skill1.SkillIsCharged && skill2.SkillIsCharged)
        {
            skill2.UpdateSkillSlot(skill1.skill, skill1.level);
            skill2.slotDisplay.GetComponentInChildren<TextMeshProUGUI>().text = "";
            SkillAudioEventTrigger.Instance.PlaySound(slotSwap);
        }
    }
    /// <summary>
    /// Swaps skills of two slots.
    /// </summary>
    /// <param name="skill1">First slot.</param>
    /// <param name="skill2">Second slot.</param>
    public void SwapSkills(SkillSlot skill1, SkillSlot skill2)
    {
        if (skill1.SkillIsCharged && skill2.SkillIsCharged)
        {
            SkillSlot temp = new SkillSlot(skill2.skill, skill2.level);
            skill2.UpdateSkillSlot(skill1.skill, skill1.level);
            skill1.UpdateSkillSlot(temp.skill, temp.level);
            skill1.slotDisplay.GetComponentInChildren<TextMeshProUGUI>().text = "";
            skill2.slotDisplay.GetComponentInChildren<TextMeshProUGUI>().text = "";
            SkillAudioEventTrigger.Instance.PlaySound(slotSwap);
        }
    }
    /// <summary>
    /// Determines custom behaviour of a callback for each of two descendants.
    /// </summary>
    public abstract void OnSkillDragEnd(GameObject obj);
    /// <summary>
    /// Ensures temporary game object following the mouse.
    /// </summary>
    /// <param name="obj">Dragged skill.</param>
    public void OnSkillDrag(GameObject obj)
    {
        if (MouseData.tempSkillBeingDragged != null)
        {
            MouseData.tempSkillBeingDragged.GetComponent<RectTransform>().position = Input.mousePosition;
        }
    }
    /// <summary>
    /// Keeps track of the interface mouse is over.
    /// </summary>
    public void OnSkillInterfaceEnter(GameObject obj)
    {
        MouseData.skillInterfaceMouseIsOver = obj.GetComponent<SkillUserInterface>();
    }
    /// <summary>
    /// Keeps track of the interface mouse is over.
    /// </summary>
    public void OnSkillInterfaceExit(GameObject obj)
    {
        MouseData.skillInterfaceMouseIsOver = null;
    }
    /// <summary>
    /// Levels up the skill and makes according changes to gameplay.
    /// </summary>
    /// <param name="_skillObject">Skill levelled up.</param>
    public virtual void LevelSkillUp(SkillObject _skillObject)
    {
        if (_skillObject.IsEligibleToLevelUp)
        {
            _skillObject.level++;
            MouseData.skillInterfaceMouseIsOver.skillsOnInterface[MouseData.skillSlotHoveredOver].UpdateSkillSlot(_skillObject.data, _skillObject.level);
        }
        OnSkillSlotUpdate(skillsOnInterface[MouseData.skillSlotHoveredOver]);
        if (_skillObject.type == SkillType.Passive)
        {
            onSkillLevelledUp?.Invoke();
        }
    }
    /// <summary>
    /// Closes UI window.
    /// </summary>
    public void CloseUIWindow()
    {
        gameObject.SetActive(false);
        button.button.image.sprite = button.closed;
        button.switcher = !button.switcher;
    }
    /// <summary>
    /// Toggles the window on/off.
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
