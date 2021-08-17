using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthCounter : MonoBehaviour
{
    private TextMeshProUGUI text;
    private PlayerHealth healthRef;
    public static HealthCounter Instance { get; private set; }

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
        healthRef = FindObjectOfType<PlayerHealth>();
        text = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateHealthCounter()
    {
        text.text = "HP: " +healthRef.Health + "/" + healthRef.MaxHealth;
    }
}
