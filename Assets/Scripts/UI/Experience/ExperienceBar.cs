using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExperienceBar : MonoBehaviour
{
    private Experience expReference;
    private Slider slider;

    private void Awake()
    {
        slider = GetComponent<Slider>();
        expReference = GameObject.FindGameObjectWithTag("ExperienceManager").GetComponent<Experience>();
    }

    public void UpdateExperienceUI()
    {
        slider.value = expReference.experience / expReference.experienceRequirement;
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
