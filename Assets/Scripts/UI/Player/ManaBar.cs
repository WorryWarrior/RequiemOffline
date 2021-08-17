using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaBar : MonoBehaviour
{
    private GeneralMana mana;
    private Slider slider;
    public static ManaBar Instance { get; private set; }
    private void Awake()
    {
        slider = GetComponent<Slider>();
        mana = FindObjectOfType<PlayerMana>();
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

    public void UpdateManaBar()
    {
        slider.value = mana.Mana / mana.MaxMana;
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
