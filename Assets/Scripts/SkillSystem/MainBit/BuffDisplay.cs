using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Class determining the behaviour of the windows representing 
/// remaining duration of buff ability.
/// </summary>
public class BuffDisplay : MonoBehaviour
{
    public Image buffImage;
    public BuffSkillObject skill;
    public TextMeshProUGUI skillName;
    public Slider slider;
    /// <summary>
    /// Sets the correct name to object instance and makes components invisible.
    /// </summary>
    private void Start()
    {
        skillName.text = skill.data.Name;
        buffImage.enabled = false;
        skillName.enabled = false;
    }
    public void Activate()
    {
        buffImage.enabled = true;
        skillName.enabled = true;
        maxTime = skill.duration;
        timeRemaining = maxTime;
        StartCoroutine(IActivate(skill.duration));
    }
    /// <summary>
    /// Coroutine disabling components when skill effect ends.
    /// </summary>
    private IEnumerator IActivate(float delay)
    {
        yield return new WaitForSeconds(delay);
        buffImage.enabled = false;
        skillName.enabled = false;
    }
    private float timeRemaining;
    private float maxTime;
    /// <summary>
    /// Allows the slider to display the correct remaining duration when it's active.
    /// </summary>
    public void Update()
    {
        slider.value = timeRemaining / maxTime;
        if (timeRemaining <= 0)
        {
            timeRemaining = 0;
        } 
        else if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
        }
    }
}
