using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Tracks current amount of skill points and updates text component accordingly.
/// </summary>
public class SkillPointCounter : MonoBehaviour
{
    private TextMeshProUGUI counter;
    private void OnEnable()
    {
        counter = GetComponent<TextMeshProUGUI>();
    }
    private void Update()
    {
        counter.text = PlayerSkill.Instance.skillPoints.ToString();
    }
}
