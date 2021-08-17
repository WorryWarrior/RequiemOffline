using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private PlayerHealth healthReference;
    private Slider slider;
    public static HealthBar Instance { get; private set; }
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
        slider = GetComponent<Slider>();
        healthReference = FindObjectOfType<PlayerHealth>();
    }

    public void UpdateHealthBar()
    {
        slider.value = healthReference.Health / healthReference.MaxHealth;
        if (slider.value < slider.minValue)
        {
            slider.value = slider.minValue;
        }
        if (slider.value > slider.maxValue)
        {
            slider.value = slider.maxValue;
        }
    }
}
