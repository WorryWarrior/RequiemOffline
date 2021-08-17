using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Skill types, each has its own functionality.
/// </summary>
public enum SkillType
{
    Active, 
    Buff, 
    Passive
}
/// <summary>
/// A general definition of a skill, shared variables are defined here.
/// </summary>
public abstract class SkillObject: ScriptableObject
{
    public Sprite activeSprite;
    public Sprite inactiveSprite;
    public SkillType type;
    public Skill data = new Skill();
    public string skillName;
    public int level;
    [TextArea(3,4)] public string description;

    public SkillObject precedingSkill;
    public float manacost;
    public float castTime;
    public float cooldown;
    public bool requiresTarget;

    /// <summary>
    /// An event firing when certain skill on a skill bar is double-clicked.
    /// </summary>
    public abstract void UseSkill();

    public bool IsEligibleToLevelUp
    {
        get
        {
            return level < 10 && (precedingSkill == null || precedingSkill.level > 0);
        }
    }
    /// <summary>
    /// Property to determine whether skill of certain type could be used.
    /// Properties below this are auxiliary and each track its own subcondition
    /// basing on which this property forms its value.
    /// </summary>
    public bool IsEligibleToCast
    {
        get
        {
            if (!requiresTarget)
                return HasEnoughMana();
            else return HasTarget();
        }
    }

    public bool HasEnoughMana()
    {
        if (FindObjectOfType<PlayerMana>() != null && FindObjectOfType<PlayerMana>().Mana >= manacost)
        {
            FindObjectOfType<PlayerMana>().ReduceMana(manacost);
            return true;
        }
        else
        {
            AnnouncementManager.Instance.CreateAnnouncement("You don't have enough mana.");
            return false;
        }
    }
    public bool HasTarget()
    {
        if (EnemyTargetManager.Instance.target != null)
        {
            return DistanceToTarget();
        }
        else
        {
            AnnouncementManager.Instance.CreateAnnouncement("You must choose the target.");
            return false;
        }
    }
    public bool DistanceToTarget()
    {
        if (Vector2.Distance(EnemyTargetManager.Instance.target.transform.position, FindObjectOfType<PlayerHealth>().gameObject.transform.position)
            < FieldOfView.Instance.viewRadius)
        {
            return TargetIsSeen();
        }
        else
        {
            AnnouncementManager.Instance.CreateAnnouncement("You are too far away.");
            return false;
        }
    }
    public bool TargetIsSeen()
    {
        if (FieldOfView.Instance.FetchTarget())
        {
            return HasEnoughMana();
        }
        else
        {
            AnnouncementManager.Instance.CreateAnnouncement("You must face the target.");
            return false;
        }
    }
}
