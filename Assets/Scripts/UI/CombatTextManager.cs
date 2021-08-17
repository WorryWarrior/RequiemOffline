using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CombatTextManager : MonoBehaviour
{
    [SerializeField] private Transform textPrefab = null;

    public static CombatTextManager Instance { get; private set; }
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
    public void SpawnText(Vector3 position, float value, Color textColour)
    {
        textPrefab.GetComponent<TextMeshPro>().color = textColour;
        Transform damageText = Instantiate(textPrefab, position, Quaternion.identity);
        SpawnCombatDamage manager = damageText.GetComponent<SpawnCombatDamage>();
        manager.CreateText(value);
    }
}
