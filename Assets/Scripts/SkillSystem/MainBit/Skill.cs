using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Class representing the skill. Determines its ID in the database
/// which allows to locate the Skill Object this skill corresponds to correctly.
/// </summary>
[System.Serializable]
public class Skill 
{
    public string Name;
    public int Id = -1;
    /// <summary>
    /// Empty object constructor.
    /// </summary>
    public Skill()
    {
        Name = "";
        Id = -1;
    }
}
