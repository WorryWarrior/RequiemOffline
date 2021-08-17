using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataManager : MonoBehaviour
{
    private PlayerHealth health = null;
    private PlayerMana mana = null;
    public PlayerInventory inventory;
    public PlayerSkill skillStorage;
    private void Awake()
    {
        health = GetComponent<PlayerHealth>();
        mana = GetComponent<PlayerMana>();
        //DataSaveLoad.LoadPlayerData();

    }
    private void Start()
    {
        //Debug.Log(DataSaveLoad.PlayerData.Level);
        //UpdateValues();
        //UpdateVariousUIs();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            DataSaveLoad.PlayerData.SetPlayerData(health.Health, mana.Mana, health.MaxHealth, mana.MaxMana, Experience.Instance.level);
            DataSaveLoad.SavePlayerData();
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            DataSaveLoad.LoadPlayerData();
            UpdateValues();
            UpdateVariousUIs();
        }
    }
    public void SaveProgress()
    {
        //DataSaveLoad.PlayerData.SetPlayerData(health.Health, mana.Mana, health.MaxHealth, mana.MaxMana, Experience.Instance.level);
        //DataSaveLoad.SavePlayerData();
        inventory.equipment.SaveEquipment();
        inventory.inventory.SaveInventory();
        skillStorage.inventory.SaveWindow();
    }
    public void RevertChanges()
    {
        //DataSaveLoad.PlayerData.SetPlayerData(health.initialHealth, mana.initialMana, health.initialHealth, mana.initialMana, 1);
        //DataSaveLoad.SavePlayerData();
        //DataSaveLoad.LoadPlayerData();
        //UpdateValues();
        //UpdateVariousUIs();
        inventory.equipment.Clear();
        inventory.equipment.SaveEquipment();
        inventory.inventory.Clear();
        inventory.inventory.SaveInventory();
        var skillPointsCounter = 0;
        for (int i = 0; i < skillStorage.inventory.GetSlots.Length; i++)
        {
            skillStorage.inventory.GetSlots[i].level = 0;
            skillPointsCounter += skillStorage.inventory.GetSlots[i].SkillObject.level;
            skillStorage.inventory.GetSlots[i].SkillObject.level = 0;
            GetComponent<PlayerSkill>().window.OnSkillSlotUpdate(skillStorage.inventory.GetSlots[i]);
        }
        PlayerSkill.Instance.skillPoints = skillPointsCounter;
        GetComponent<PlayerSkill>().window.UpdateUIArrows();
        skillStorage.inventory.SaveWindow();
        skillStorage.barObject.Clear();
    }
    public void LoadProgress()
    {
        //DataSaveLoad.LoadPlayerData();
        //UpdateValues();
        //UpdateVariousUIs();
        inventory.inventory.LoadInventory();
        inventory.equipment.LoadEquipment();
        skillStorage.inventory.LoadWindow();
    }
    public void Quit()
    {
        Application.Quit();
    }
    private void UpdateVariousUIs()
    {
        health.UpdateHealthUI();
        mana.UpdateManaUI();
        Experience.Instance.UpdateExpUI();
    }
    private void UpdateValues()
    {
        health.Health = DataSaveLoad.PlayerData.Health;
        mana.Mana = DataSaveLoad.PlayerData.Mana;
        health.MaxHealth = DataSaveLoad.PlayerData.MaxHealth + 2 * DataSaveLoad.PlayerData.Level;
        mana.MaxMana = DataSaveLoad.PlayerData.MaxMana;
        Experience.Instance.level = DataSaveLoad.PlayerData.Level;
    }
}
