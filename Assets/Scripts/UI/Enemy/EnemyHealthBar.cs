using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] private EnemyHealth targetHealth = null;
    private Slider slider;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }
    private void Update()
    {
        if (EnemyTargetManager.Instance.target != null)
        {
            targetHealth = EnemyTargetManager.Instance.target.GetComponent<EnemyHealth>();
            UpdateEnemyHealthBar();
        }
    }
    public void UpdateEnemyHealthBar()
    {
        slider.value = targetHealth.Health / targetHealth.MaxHealth;
        if (slider.value < slider.minValue)
        {
            slider.value = slider.minValue;
        }
        if (slider.value > slider.maxValue)
        {
            slider.value = slider.maxValue;
        }
        if (targetHealth.Health <= 0) gameObject.SetActive(false);
    }
}
