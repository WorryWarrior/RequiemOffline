using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.IO;

/// <summary>
/// Delegate used to trigger single slot events instead of looping through all slots.
/// </summary>
public delegate void SkillSlotUpdated(SkillSlot _slot);

/// <summary>
/// Scriptable Object used for saving and loading skill-related data.
/// </summary>
[CreateAssetMenu(fileName = "New Skill Storage Object", menuName = "SkillSystem/Storage")]
public class SkillInventoryObject : ScriptableObject
{
    public SkillDatabaseObject database;
    public SkillInventory Container;
    public Action onDeserialization;
    public SkillSlot[] GetSlots { get => Container.Slots; }

    [ContextMenu("SaveWindow")]
    public void SaveWindow()
    {
        IFormatter formatter = new BinaryFormatter();
        FileStream file = new FileStream(GetDataPath(), FileMode.OpenOrCreate);
        formatter.Serialize(file, Container);
        file.Close();
    }
    [ContextMenu("LoadWindow")]
    public void LoadWindow()
    {
        if (File.Exists(GetDataPath()))
        {
            IFormatter formatter = new BinaryFormatter();
            FileStream file = new FileStream(GetDataPath(), FileMode.Open);
            SkillInventory proxyContainer = (SkillInventory)formatter.Deserialize(file);
            for (int i = 0; i < GetSlots.Length; i++)
            {
                GetSlots[i].SkillObject.level = proxyContainer.Slots[i].level;
                GetSlots[i].UpdateSkillSlot(proxyContainer.Slots[i].skill, proxyContainer.Slots[i].level);
            }
            file.Close();
        }
        onDeserialization?.Invoke();
    }
    
    public string GetDataPath()
    {
        return Application.persistentDataPath + "/data2.bf";
    }

    [ContextMenu("Clear")]
    public void Clear()
    {
        for (int i = 0; i < GetSlots.Length; i++)
        {
            GetSlots[i].RemoveSkill();
        }
    }
}
/// <summary>
/// Skill storage.
/// </summary>
[Serializable]
public class SkillInventory
{
    public SkillSlot[] Slots = new SkillSlot[12];
    public void Clear()
    {
        for (int i = 0; i < Slots.Length; i++)
        {
            Slots[i].UpdateSkillSlot(new Skill(), 0);
        }
    }
}