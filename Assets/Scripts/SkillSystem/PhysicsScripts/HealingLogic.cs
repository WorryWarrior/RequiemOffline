using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Restores player's health and destroys the prefab.
/// </summary>
public class HealingLogic : MonoBehaviour
{
    public ActiveSkillObject skill;
    private void Start()
    {
        FindObjectOfType<PlayerHealth>()?.GainHealth(skill.level);
        Destroy(gameObject);
    }
}
