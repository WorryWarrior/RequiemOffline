using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// Class determining the behaviour of the button increasing certain skill's level.
/// </summary>
public class LevelSkillButton : MonoBehaviour, IPointerClickHandler
{
    public SkillObject skillToLevelUp;
    public SkillWindow _interface;
    public Button button;
    public AudioClip skillLevelledUp;
    /// <summary>
    /// Increases the level of assigned skill and fires a sound.
    /// </summary>
    public void OnPointerClick(PointerEventData eventData)
    {
        if (PlayerSkill.Instance.SpendSkillPoint())
        {
            _interface.LevelSkillUp(skillToLevelUp);
            SkillAudioEventTrigger.Instance.PlaySound(skillLevelledUp);
        }
    }
    public void SetInactive()
    {
        button.image.enabled = false;
    }
    public void SetActive()
    {
        button.image.enabled = true;
    }
}
