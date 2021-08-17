using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageReduction : MonoBehaviour
{
    public SkillUserInterface window;
    public SkillInventoryObject storage;
    public SkillObject skillToCorrelate;
    private void OnEnable()
    {
        window.onSkillLevelledUp += EnableDamageReduction;
        storage.onDeserialization += EnableDamageReduction;
    }
    private void OnDisable()
    {
        window.onSkillLevelledUp -= EnableDamageReduction;
        storage.onDeserialization -= EnableDamageReduction;
    }

    private void EnableDamageReduction()
    {
        foreach (var enemy in FindObjectsOfType<RoamingEnemyBehaviour>())
        {
            enemy.damageReducingConstant = skillToCorrelate.level;
        }
    }
}
