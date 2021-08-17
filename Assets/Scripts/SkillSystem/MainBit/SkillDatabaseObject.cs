using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Scriptable object for assigning each skill a unique ID.
/// </summary>
[CreateAssetMenu(fileName = "New Skill Database", menuName = "SkillSystem/SkillDatabase")]
public class SkillDatabaseObject : ScriptableObject, ISerializationCallbackReceiver
{
    public SkillObject[] skillObjects;

    public void OnAfterDeserialize()
    {
        for (int i = 0; i < skillObjects.Length; i++)
        {
            skillObjects[i].data.Id = i;
        }
    }

    public void OnBeforeSerialize()
    {
    }
}
