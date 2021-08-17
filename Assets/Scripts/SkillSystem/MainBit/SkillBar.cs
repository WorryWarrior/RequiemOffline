using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Linq;

/// <summary>
/// Script determining the behavour of different activities with skill bar.
/// </summary>
public class SkillBar : SkillUserInterface
{
    public GameObject[] slots;
    /// <summary>
    /// Applies a set of callbacks to every slots and fills in the crucial dictionary for proper 
    /// callback functioning.
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
    /// Destroys temporarily instantiated prefab, swaps skills if grabbed skill was not empty,
    /// removes the skill from the bar if it's charged and mouse is not over any interface.
    /// </summary>
    /// <param name="obj">Slot</param>
    public override void OnSkillDragEnd(GameObject obj)
    {
        skillIsDragged = false;
        Destroy(MouseData.tempSkillBeingDragged);
        if (MouseData.skillSlotHoveredOver && MouseData.skillInterfaceMouseIsOver.GetComponent<SkillWindow>() == null &&
            lastSkillSlot.SkillObject != null)
        {
            SkillSlot mouseHoverSkillSlotData = MouseData.skillInterfaceMouseIsOver.skillsOnInterface[MouseData.skillSlotHoveredOver];
            SwapSkills(MouseData.skillInterfaceMouseIsOver.skillsOnInterface[obj], mouseHoverSkillSlotData);
        }
        if (MouseData.skillInterfaceMouseIsOver == null && lastSkillSlot.parent.GetComponent<SkillWindow>() == null && 
            lastSkillSlot.SkillIsCharged)
        {
            skillsOnInterface[obj].RemoveSkill();
        }
    }
    private int clicked = 0;
    private float clickTime = 0;
    private float doubleClickTime = 0.5f;
    /// <summary>
    /// Handles double-click mechanics, if the skill is charged uses the skill.
    /// </summary>
    /// <param name="obj">Slot</param>
    public override void OnSkillClick(GameObject obj)
    {
        base.OnSkillClick(obj);
        if (MouseData.skillSlotHoveredOver != null && skillsOnInterface[MouseData.skillSlotHoveredOver].SkillObject != null &&
            skillsOnInterface[MouseData.skillSlotHoveredOver].SkillIsCharged && FindObjectOfType<PlayerHealth>() != null)
        {
            clicked++;
            if (clicked == 1)
                clickTime = Time.time;
            if (clicked == 2 && Time.time - clickTime < doubleClickTime)
            {
                var currentSlot = skillsOnInterface[MouseData.skillSlotHoveredOver];
                if (currentSlot.SkillObject.IsEligibleToCast)
                {
                    currentSlot.SkillObject.UseSkill();
                    HandleFilling(currentSlot);
                }
                clicked = 0;
            }
            else if (clicked > 2 || Time.time - clickTime > 1)
                clicked = 0;
                
        }
    }
    [HideInInspector] public List<SkillSlot> skillsToFill = new List<SkillSlot>();
    /// <summary>
    /// Adds a skill to the list of skills being filled up every frame and instantiates 
    /// an inactive copy of the skill.
    /// </summary>
    /// <param name="_slot">Slots</param>
    public void HandleFilling(SkillSlot _slot)
    {
        skillsToFill.Add(_slot);
        _slot.slotDisplay.transform.GetChild(0).GetComponentInChildren<Image>().fillAmount = 0;
        var tempInactiveSkill = CreateInactiveCopy(_slot);
        tempInactiveSkill.transform.SetParent(MouseData.skillSlotHoveredOver.transform);
        tempInactiveSkill.transform.SetAsFirstSibling();
        tempInactiveSkill.transform.position = MouseData.skillSlotHoveredOver.transform.position;
        Destroy(tempInactiveSkill, _slot.SkillObject.cooldown);
    }
    /// <summary>
    /// Instantiates a prefabs and fills its components accordingly.
    /// </summary>
    /// <param name="_slot">Target slot</param>
    /// <returns>Prefab with sprite of inactive slot.</returns>
    private GameObject CreateInactiveCopy(SkillSlot _slot)
    {
        var canvas = FindObjectOfType<Canvas>();
        GameObject tempSkill = Instantiate(tempSkillPrefab, MouseData.skillSlotHoveredOver.transform.position, Quaternion.identity);
        tempSkill.GetComponent<RectTransform>().sizeDelta = new Vector2(30 * canvas.scaleFactor, 30 * canvas.scaleFactor);
        tempSkill.transform.SetParent(transform.parent);
        var image = tempSkill.GetComponent<Image>();
        image.sprite = _slot.SkillObject.inactiveSprite;
        image.raycastTarget = false;
        return tempSkill;
    }
    /// <summary>
    /// Shortcut property to check whether a skill was filled.
    /// </summary>
    /// <param name="_slot">Slot to check</param>
    public bool IsFilled(SkillSlot _slot)
    {
        foreach (var skill in skillsToFill)
        {
            if (skill.SkillObject == _slot.SkillObject)
                return false;
        }
        return true;
    }
    /// <summary>
    /// Fills the images of all non-filled skills and removes it from the list upon filling.
    /// </summary>
    private void Update()
    {
        for (int i = 0; i < skillsToFill.Count; i++)
        {
            var skill = skillsToFill.ElementAt(i);
            if (skill.SkillIsCharged)
            {
                skill.slotDisplay.transform.GetChild(1).GetComponentInChildren<Image>().fillAmount = 1;
                skillsToFill.Remove(skill);
            }
            else
            {
                if (skill.slotDisplay.transform.GetChild(1).GetComponentInChildren<Image>() != null)
                {
                    skill.slotDisplay.transform.GetChild(1).GetComponentInChildren<Image>().fillAmount +=
                     1.0f / skill.SkillObject.cooldown * Time.deltaTime;
                } else
                {
                    Debug.Log("You messed up");
                }
            }
        }
    }
    /// <summary>
    /// Clears up the bar when game session is over.
    /// </summary>
    private void OnApplicationQuit()
    {
        foreach (var skill in inventory.GetSlots)
        {
            skill.RemoveSkill();
        }
    }
}
