using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerSkill : MonoBehaviour
{
    public SkillInventoryObject inventory;
    public SkillInventoryObject barObject;
    public SkillBar bar;
    public SkillWindow window;
    public int skillPoints;
    public static PlayerSkill Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void GainSkillPoint()
    {
        skillPoints++;
    }
    public bool SpendSkillPoint()
    {
        if (skillPoints > 0)
        {
            skillPoints--;
            return true;
        }
        return false;
    }
   
    public void PerformSkillDelayedLogic(Action _skillLogic, float _delay)
    {
        StartCoroutine(SkillCoroutine(_delay, _skillLogic));
    }
    private IEnumerator SkillCoroutine(float delay, Action action)
    {
        yield return new WaitForSeconds(delay);
        action();
    }
    public void ActivateDurationIcon(SkillObject _skillObject)
    {
        foreach (var icon in FindObjectsOfType<BuffDisplay>())
        {
            if (icon.skill == _skillObject)
            {
                icon.Activate();
            }
        }
    }
    public void OnApplicationQuit()
    {
        foreach (var skill in inventory.Container.Slots)
        {
            skill.SkillObject.level = 0;
            skill.UpdateSkillSlot(skill.skill, 0);
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L)) 
        {
            inventory.SaveWindow();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            inventory.LoadWindow();
            window.UpdateUIArrows();
        }
    }
}
