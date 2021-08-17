using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class DataSaveLoad : MonoBehaviour
{
    public static PlayerData PlayerData { get; private set; }
    public static void LoadPlayerData()
    {
        PlayerData = new PlayerData();
        if (File.Exists(GetPlayerDataPath()))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream file = new FileStream(GetPlayerDataPath(), FileMode.Open);
            PlayerData = (PlayerData)formatter.Deserialize(file);
            file.Close();
        }
    }

    public static void SavePlayerData()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream file = new FileStream(GetPlayerDataPath(), FileMode.OpenOrCreate);
        formatter.Serialize(file, PlayerData);
        file.Close();
    }

    public static string GetPlayerDataPath()
    {
        return Application.persistentDataPath + "/playerdata.bf";
    }
    
    /*public static Transform player;
    private void Start()
    {
        player = FindObjectOfType<PlayerHealth>().gameObject.transform;
    }
    public static void SaveData(PlayerDataManager test)
    {
        PlayerPrefs.SetFloat("x", player.position.x);
        PlayerPrefs.SetFloat("y", player.position.y);
        PlayerPrefs.SetFloat("health", FindObjectOfType<PlayerHealth>().Health);
        PlayerPrefs.SetFloat("mana", FindObjectOfType<PlayerMana>().Mana);
        PlayerPrefs.SetFloat("maxHealth", FindObjectOfType<PlayerHealth>().MaxHealth);
        PlayerPrefs.SetFloat("maxMana", FindObjectOfType<PlayerMana>().MaxMana);
        PlayerPrefs.SetInt("level", Experience.Instance.level);
    }
    public static void UndoChanges(PlayerDataManager test)
    {
        PlayerPrefs.SetFloat("x", -10);
        PlayerPrefs.SetFloat("y", -3.5f);
        PlayerPrefs.SetFloat("health", 10);
        PlayerPrefs.SetFloat("mana", 15);
        PlayerPrefs.SetFloat("maxHealth", 10);
        PlayerPrefs.SetFloat("maxMana", 15);
        PlayerPrefs.SetInt("level", 1);
    }
    public static PlayerData LoadData()
    {
        float x = PlayerPrefs.GetFloat("x");
        float y = PlayerPrefs.GetFloat("y");
        float health = PlayerPrefs.GetFloat("health");
        float mana = PlayerPrefs.GetFloat("mana");
        int level = PlayerPrefs.GetInt("level");
        float maxHealth = PlayerPrefs.GetFloat("maxHealth");
        float maxMana = PlayerPrefs.GetFloat("maxMana");
        PlayerData playerData = new PlayerData()
        {
            Location = new Vector3(x, y),
            Health = health,
            Mana = mana,
            Level = level,
            MaxHealth = maxHealth,
            MaxMana = maxMana,
        };
        return playerData;
    }
}*/
}
[Serializable]
public class PlayerData
{
    public float Health { get; set; }
    public float Mana { get; set; }
    public float MaxHealth { get; set; }
    public float MaxMana { get; set; }
    public int Level { get; set; }
    public void SetPlayerData(float _health, float _mana, float _maxHealth, float _maxMana, int _level)
    {
        Health = _health;
        Mana = _mana;
        MaxHealth = _maxHealth;
        MaxMana = _maxMana;
        Level = _level;
    }
}
