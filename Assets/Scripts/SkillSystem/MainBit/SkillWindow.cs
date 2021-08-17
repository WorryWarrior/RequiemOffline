using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Extends some methods of the parent and adds new functionality.
/// </summary>
public class SkillWindow : SkillUserInterface
{
    public GameObject[] slots;
    public SkillPopup popup;

    /// <summary>
    /// Extends a base method with UI components of this window.
    /// </summary>
    public override void Start()
    {
        base.Start();
        UpdateUIArrows();
        OpenUIWindow();
    }
    /// <summary>
    /// Fills in the GameObject-SkillSlot dictionary, adds a set of callbacks to 
    /// each slot.
    /// </summary>
    public override void CreateSlots()
    {
        skillsOnInterface = new Dictionary<GameObject, SkillSlot>();
        for (int i = 0; i < inventory.Container.Slots.Length; i++)
        {
            var obj = slots[i];
            AddEvent(obj, EventTriggerType.PointerEnter, delegate { OnSkillEnter(obj); });
            AddEvent(obj, EventTriggerType.PointerExit, delegate { OnSkillExit(obj); });
            AddEvent(obj, EventTriggerType.BeginDrag, delegate { OnSkillDragStart(obj); });
            AddEvent(obj, EventTriggerType.EndDrag, delegate { OnSkillDragEnd(obj); });
            AddEvent(obj, EventTriggerType.Drag, delegate { OnSkillDrag(obj); });
            AddEvent(obj, EventTriggerType.PointerDown, delegate { OnSkillClick(obj); });
            inventory.GetSlots[i].slotDisplay = obj;
            skillsOnInterface.Add(obj, inventory.Container.Slots[i]);
        }
    }
    /// <summary>
    /// Checks whether skill attempted to drop is eligible for that, 
    /// e.g. you cannot drop passive abilities, uncharged skills, empty slots.
    /// Ensures correct interaction between skill bar and skill window storages.
    /// Removes the skill if mouse is not over any interface.
    /// </summary>
    /// <param name="obj">Initial slot.</param>
    public override void OnSkillDragEnd(GameObject obj)
    {
        skillIsDragged = false;
        Destroy(MouseData.tempSkillBeingDragged);
        if (MouseData.skillSlotHoveredOver && MouseData.skillInterfaceMouseIsOver.GetComponent<SkillWindow>() == null &&
            lastSkillSlot.SkillObject != null && lastSkillSlot.SkillObject.level != 0 && lastSkillSlot.SkillObject.type != SkillType.Passive &&
            FindObjectOfType<SkillBar>().IsFilled(lastSkillSlot))
        {
            SkillSlot mouseHoverSkillSlotData = MouseData.skillInterfaceMouseIsOver.skillsOnInterface[MouseData.skillSlotHoveredOver];
            for (int i = 0; i < bar.Container.Slots.Length; i++)
            {
                var slot = bar.Container.Slots[i];
                if (slot.SkillObject == lastSkillSlot.SkillObject)
                {
                    slot.RemoveSkill();
                }
            }
            CopySkill(skillsOnInterface[obj], mouseHoverSkillSlotData);
        }
    }
    /// <summary>
    /// Extends the base method and updates the popup position and values.
    /// </summary>
    /// <param name="obj">Entered slot.</param>
    public override void OnSkillEnter(GameObject obj)
    {
        base.OnSkillEnter(obj);
        popup.UpdatePosition(MouseData.skillSlotHoveredOver.GetComponent<RectTransform>().anchoredPosition, 150, 85);
        UpdatePopup();
    }
    /// <summary>
    ///  Extends the base method and sets the popup invisible.
    /// </summary>
    /// <param name="obj">Exited slot.</param>
    public override void OnSkillExit(GameObject obj)
    {
        base.OnSkillExit(obj);
        popup.SetComponentsInvisible();
    }
    /// <summary>
    /// Ensures correct visibility of skill level up buttons.
    /// </summary>
    private void Update()
    {
        EnableProperSkillLevelButtons();
    }
    /// <summary>
    /// Determines the visibility of every skill level up button.
    /// </summary>
    public void EnableProperSkillLevelButtons()
    {
        foreach (var button in FindObjectsOfType<LevelSkillButton>())
        {
            if (button.skillToLevelUp.IsEligibleToLevelUp && PlayerSkill.Instance.skillPoints != 0)
            {
                button.SetActive();
            }
            else
            {
                button.SetInactive();
            }
        }
    }
    /// <summary>
    /// Extends the parent method with window's UI methods.
    /// </summary>
    /// <param name="_skillObject">Levelled skill.</param>
    public override void LevelSkillUp(SkillObject _skillObject)
    {
        base.LevelSkillUp(_skillObject);
        UpdateUIArrows();
        UpdatePopup();
    }
    /// <summary>
    /// Enables popup and passes a Skill Object of currently hovered skill.
    /// </summary>
    private void UpdatePopup()
    {
        popup.SetComponentsVisible(skillsOnInterface[MouseData.skillSlotHoveredOver].SkillObject);
    }
    /// <summary>
    /// Updates the state of each UI arrow.
    /// </summary>
    public void UpdateUIArrows()
    {
        foreach (var arrow in FindObjectsOfType<ArrowHandler>())
        {
            arrow.UpdateArrowState();
        }
    }
}
