using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// One of three skilll type scripts which are responsible for correct instantiation
/// of Scriptable Object as well as specify skill behaviour. Note that this script is a part
/// of generic structure and used for all active skills in the scene. Each skill's own behaviour
/// is determined by its own script.
/// </summary>
[CreateAssetMenu(fileName = "New Active Skill Object", menuName = "SkillSystem/Skills/Active")]
public class ActiveSkillObject : SkillObject
{
    public GameObject prefab;
    public AudioClip success;
    public AudioClip cast;
    private void Awake()
    {
        type = SkillType.Active;
    }
    /// <summary>
    /// Determines what happens when skill on skill bar is pressed twice. 
    /// Fires the audio, disables player movement, enables casting UI.
    /// Doesn't fire the skill.
    /// </summary>
    public override void UseSkill()
    {
        if (castTime == 0)
            SkillLogic();
        else
        {
            CastingUI.Instance.SetCastTime(castTime);
            TopDownMovementScript.Instance.DisablePlayerMovement(castTime);
            SkillAudioEventTrigger.Instance.PlayCastingSound(cast);
            PlayerSkill.Instance.PerformSkillDelayedLogic(SkillLogic, castTime);
        }
    }
    /// <summary>
    /// Method that fires a skill, used instantly in case of skill cast time equial zero
    /// or passed as delegate to MonoBehaviour proxy since Scriptable Objects cannot call coroutines.
    /// </summary>
    private void SkillLogic()
    {
        if (prefab != null) 
            Instantiate(prefab, FindObjectOfType<PlayerHealth>().transform.position, Quaternion.identity);
        SkillAudioEventTrigger.Instance.PlaySound(success);
    }
}
