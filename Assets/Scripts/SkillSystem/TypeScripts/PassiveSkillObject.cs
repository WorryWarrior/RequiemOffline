using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// One of three skilll type scripts which are responsible for correct instantiation
/// of Scriptable Object as well as specify skill behaviour.
/// <see cref="ActiveSkillObject"/> for more info.
/// </summary>
[CreateAssetMenu(fileName = "New Passive Skill Object", menuName = "SkillSystem/Skills/Passive")]
public class PassiveSkillObject : SkillObject
{
    private void Awake()
    {
        type = SkillType.Passive;
    }
    /// <summary>
    /// Doesn't do anything since skills of this type cannot be dragged to skill bar, skill is never pressed
    /// which means this method is never called. The logic of these skills is sealed in other scripts.
    /// </summary>
    public override void UseSkill()
    {
        
    }
}
