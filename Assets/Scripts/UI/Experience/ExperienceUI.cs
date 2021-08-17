using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ExperienceUI : MonoBehaviour
{
    private Experience expRef;
    private TextMeshProUGUI text;

    private void Awake()
    {
        expRef = GameObject.FindGameObjectWithTag("ExperienceManager").GetComponent<Experience>();
        text = GetComponent<TextMeshProUGUI>();
    }
    public void UpdateText()
    {
        text.text = Mathf.Floor(((expRef.experience / expRef.experienceRequirement) * 100))+ "%";
    }
}
