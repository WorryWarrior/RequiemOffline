using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Restores player's mana and destroys the prefab.
/// </summary>
public class MentalRejuvenationLogic : MonoBehaviour
{
    public BuffSkillObject skill;
    private void Start()
    {
        FindObjectOfType<PlayerMana>()?.RestoreMana(3 * skill.level);
        Destroy(gameObject);
    }
}
