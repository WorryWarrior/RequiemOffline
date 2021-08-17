using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Skill slot representation storing all data required for correct 
/// functionality.
/// </summary>
[Serializable]
public class SkillSlot
{
    [NonSerialized]
    public SkillUserInterface parent;
    [NonSerialized]
    public GameObject slotDisplay;
    [NonSerialized]
    public SkillSlotUpdated onAfterUpdate;
    public Skill skill;
    public int level;

    /// <summary>
    /// Property retrieving Skill Object of a skill stored via skill database. 
    /// </summary>
    public SkillObject SkillObject
    {
        get
        {
            if (skill.Id >= 0)
            {
                return parent.inventory.database.skillObjects[skill.Id];
            }
            else return null;
        }
    }
    /// <summary>
    /// A set of conditions used as a shortcut for checking whether stored skill
    /// was visually charged. Note that uncharged skills do not support any kind of interaction
    /// due to simplicity of the system.
    /// </summary>
    public bool SkillIsCharged
    {
        get
        {
            if (slotDisplay.transform.GetChild(0).GetComponentInChildren<Image>() != null &&
                slotDisplay.transform.GetChild(1).GetComponentInChildren<Image>() != null &&
                slotDisplay.transform.GetChild(0).GetComponentInChildren<Image>().fillAmount > 0.989f &&
                slotDisplay.transform.GetChild(1).GetComponentInChildren<Image>().fillAmount > 0.989f ||

                slotDisplay.transform.GetChild(0).GetComponentInChildren<Image>() != null &&
                slotDisplay.transform.GetChild(1).GetComponentInChildren<Image>() == null &&
                slotDisplay.transform.GetChild(0).GetComponentInChildren<Image>().fillAmount > 0.989f
            )
            return true;
            else
            {
                return false;
            }
        }

    }
    /// <summary>
    /// A constructor to set up an object.
    /// </summary>
    /// <param name="_skill">Stored skill.</param>
    /// <param name="_level">Stored skill's level.</param>
    public SkillSlot(Skill _skill, int _level)
    {
        UpdateSkillSlot(_skill, _level);
    }
    /// <summary>
    /// Updates the state of existing slot.
    /// </summary>
    /// <param name="_skill">Stored skill.</param>
    /// <param name="_level">Stored skill's level.</param>
    public void UpdateSkillSlot(Skill _skill, int _level)
    {
        skill = _skill;
        level = _level;
        onAfterUpdate?.Invoke(this);
    }
    /// <summary>
    /// Clears the slot.
    /// </summary>
    public void RemoveSkill()
    {
        UpdateSkillSlot(new Skill(), 0);
    }
}
